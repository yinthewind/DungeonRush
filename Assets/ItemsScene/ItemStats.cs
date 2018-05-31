using System;
using System.Collections.Generic;

public enum PositionCategory {
	Nowhere,
	MainHand,
	OffHand,
	Body,
	Amulate,
	Backpack,
};

public class Position {
	public PositionCategory Category;
	public int Index;

	public Position(PositionCategory cat, int idx) {
		this.Category = cat;
		this.Index = idx;
	}

	public override int GetHashCode() {
		return Index * Enum.GetNames (typeof(PositionCategory)).Length + (int)Category;
	}

	public override bool Equals(System.Object obj) 
	{
		// Check for null values and compare run-time types.
		if (obj == null || GetType() != obj.GetType()) 
			return false;

		Position p = (Position)obj;
		return (Category == p.Category) && (Index == p.Index);
	}
}

public class ItemStats {
	Dictionary<Position, Item> items;
	Dictionary<PositionCategory, Func<Position, Item, bool>> checkers;

	public ItemStats() {
		items = new Dictionary<Position, Item> ();

		Func<Position, Item, bool> dummyChecker = (Position position, Item item) => {
			return true;
		};
		checkers = new Dictionary<PositionCategory, Func<Position, Item, bool>> () { { 
				PositionCategory.Amulate, (Position position, Item item) => {
					if (position.Index == 0) {
						return item.Category == ItemCategory.Amulate;
					} else if (this.GetItem(position.Category, 0) == null) {
						return false;
					} else {
						return item.Category == ItemCategory.Gem;
					}
				}
			}, { 
				PositionCategory.Backpack, dummyChecker
			}, { 
				PositionCategory.Body, (Position position, Item item) => {
					if (position.Index == 0) {
						return item.Category == ItemCategory.Armor;
					} else if (this.GetItem(position.Category, 0) == null) {
						return false;
					} else {
						return item.Category == ItemCategory.Gem;
					}
				}
			}, {
				PositionCategory.MainHand, (Position position, Item item) => {
					if (position.Index == 0) {
						return item.Category == ItemCategory.Weapon;
					} else if (this.GetItem(position.Category, 0) == null) {
						return false;
					} else {
						return item.Category == ItemCategory.Gem;
					}
				}
			}, {
				PositionCategory.OffHand, (Position position, Item item) => {
					if (position.Index == 0) {
						return item.Category == ItemCategory.Weapon || item.Category == ItemCategory.Shield;
					} else if (this.GetItem(position.Category, 0) == null) {
						return false;
					} else {
						return item.Category == ItemCategory.Gem;
					}
				}
			},
		};
	}

	public Item GetWeapon() {
		return this.GetItem (new Position (PositionCategory.MainHand, 0));
	}

	public Item GetArmor() {
		return this.GetItem (new Position (PositionCategory.Body, 0));
	}

	public Item GetAmulate() {
		return this.GetItem (new Position (PositionCategory.Amulate, 0));
	}

	public Dictionary<Position, Item>.ValueCollection GetItems() {
		return items.Values;
	}

	public bool Put(Position pos, Item item) {

		var thatItem = GetItem (pos);
		var thisDest = pos;
		var thatDest = item.Pos;

		var thisChecker = checkers[thisDest.Category];
		var thatChecker = checkers [thatDest.Category];
		if (thatItem != null) { // swapping two items
			if (thisChecker (thisDest, item) && thatChecker (thatDest, thatItem)) {

				put (thatDest, thatItem);
				put (thisDest, item);

				return true;
			} else {
				return false;
			}
		} else { // put
			if (thisChecker (thisDest, item)) {
				take (thatDest);
				put (thisDest, item);

				return true;
			} else {
				return false;
			}
		}
	}

	void put(Position pos, Item item) {
		if (!this.items.ContainsKey (pos)) {
			this.items.Add (pos, item);
		} else {
			this.items [pos] = item;
		}
		item.Pos = pos;
	}

	void take(Position pos) {
		items [pos] = null;
	}

	public Item GetItem(PositionCategory category, int index) {
		return this.GetItem (new Position (category, index));
	}

	public Item GetItem(Position pos) {
		if (items.ContainsKey (pos) == false) {
			return null;
		}
		return items [pos];
	}

	bool isVacant(Position pos) {
		return items.ContainsKey(pos) == false || items[pos] == null;
	}

	public void AddToBackpack(Item item) {
		for (int i = 0; i < 60; i++) {
			var pos = new Position (PositionCategory.Backpack, i);
			if (isVacant (pos)) {
				item.Pos = pos;
				put (pos, item);

				return;
			}
		}
	}

	public List<CardType> GetDeck() {

		var deck = new List<CardType> ();

		var mainHandCards = getMainHandCards ();
		var offHandCards = getOffHandCards ();
		var bodyCards = getBodyCards ();
		var amulateCards = getAmulateCards ();

		deck.AddRange (mainHandCards);
		deck.AddRange (offHandCards);
		deck.AddRange (bodyCards);
		deck.AddRange (amulateCards);

		return deck;
	}

	List<CardType> mainHandDefaultCards = new List<CardType>() {
		CardType.DeadlyPunch,
		CardType.Punch,
		CardType.Punch,
		CardType.Punch,
		CardType.Miss,
		CardType.Miss,
	};

	Dictionary<PositionCategory, List<CardType>> defaultCards = new Dictionary<PositionCategory, List<CardType>>() { {
			PositionCategory.MainHand, new List<CardType>() {
				CardType.DeadlyPunch,
				CardType.Punch,
				CardType.Punch,
				CardType.Punch,
				CardType.Miss,
				CardType.Miss,
			}
		}, {
			PositionCategory.OffHand, new List<CardType>() {
				CardType.DeadlyPunch,
				CardType.Punch,
				CardType.Punch,
				CardType.Punch,
				CardType.Miss,
				CardType.Miss,
			}
		}, {
			PositionCategory.Body, new List<CardType>()
		}, {
			PositionCategory.Amulate, new List<CardType>()
		},
	};

	List<Item> getEquipments(PositionCategory category) {
		var result = new List<Item> ();
		for(int i = 0;i < 5; i++) {
			var item = this.GetItem (category, i);
			if(item != null) {
				result.Add (item);
			}
		}
		return result;
	}

	List<Item> getMainHandItems() {
		return getEquipments(PositionCategory.MainHand);
	}

	List<Item> getOffHandItems() {
		return getEquipments (PositionCategory.OffHand);
	}

	List<Item> getBodyItems() {
		return getEquipments (PositionCategory.Body);
	}

	List<Item> getAmulateItems() {
		return getEquipments (PositionCategory.Amulate);
	}

	public List<Item> GetAllEquipments() {
		var result = new List<Item> ();
		result.AddRange (getMainHandItems ());
		result.AddRange (getOffHandItems ());
		result.AddRange (getBodyItems ());
		result.AddRange (getAmulateItems ());
		return result;
	}

	List<CardType> getEquipmentCards(PositionCategory category) {
		var items = this.getEquipments (category);

		if(items.Count == 0) {
			return defaultCards [category];
		}

		var result = new List<CardType> ();
		foreach(var item in items) {
			if (item.Cards == null) {
				continue;
			}
			result.AddRange(item.Cards);
		}
		return result;
	}

	List<CardType> getMainHandCards() {
		return getEquipmentCards (PositionCategory.MainHand);
	}

	List<CardType> getOffHandCards() {
		return getEquipmentCards (PositionCategory.OffHand);
	}

	List<CardType> getBodyCards() {
		return getEquipmentCards (PositionCategory.Body);
	}

	List<CardType> getAmulateCards() {
		return getEquipmentCards (PositionCategory.Amulate);
	}
}
﻿using System;
using System.Linq;
using System.Collections.Generic;

public enum PositionCategory {
	Nowhere,
	MainHand,
	OffHand,
	Body,
	Amulate,
	Backpack,
	Rewards,
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
	ItemFactory itemFactory;

	public bool Add(Position position, ItemType itemType) {
		var item = this.itemFactory.Create(itemType);
		if(this.GetItem(position) == null) {
			this.Put(position, item);
			return true;
		}
		return false;
	}

	public ItemStats() {
		itemFactory = new ItemFactory();

		items = new Dictionary<Position, Item> ();

		Func<Position, Item, bool> dummyChecker = (Position position, Item item) => {
			return true;
		};
	}

	public Dictionary<Position, Item> Items() {
		return this.items.Where(kv => kv.Value != null).ToDictionary(kv => kv.Key, kv => kv.Value);
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

	public List<Item> GetItems() {
		return this.items.Select(kv => kv.Value).Where(x => x != null).ToList();
	}

	public bool AddItemToBackpack(Item item) {
		for(int i = 0; i < 24; i++) {
			var pos = new Position(PositionCategory.Backpack, i);
			if (this.isVacant(pos)) {
				this.Put(pos, item);
				return true;
			}
		}
		return false;
	}

	public bool Take(Position pos) {
		if (!items.ContainsKey (pos)) {
			return false;
		}
		items[pos] = null;
		return true;
	}

	public void Put(Position pos, Item item) {
		if (!this.items.ContainsKey (pos)) {
			this.items.Add (pos, item);
		} else {
			this.items [pos] = item;
		}
		item.Position = pos;
	}

	public Item GetItem(PositionCategory category, int index) {
		return this.GetItem (new Position (category, index));
	}

	public Item GetItem(Position pos) {
		if (pos == null) {
			return null;
		}
		if (items.ContainsKey (pos) == false) {
			return null;
		}
		return items [pos];
	}

	bool isVacant(PositionCategory category, int index) {
		return this.isVacant(new Position(category, index));
	}
	bool isVacant(Position pos) {
		return items.ContainsKey(pos) == false || items[pos] == null;
	}

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

	public List<Item> GetEquipments(PositionCategory category) {
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
		return GetEquipments(PositionCategory.MainHand);
	}

	List<Item> getOffHandItems() {
		return GetEquipments (PositionCategory.OffHand);
	}

	List<Item> getBodyItems() {
		return GetEquipments (PositionCategory.Body);
	}

	List<Item> getAmulateItems() {
		return GetEquipments (PositionCategory.Amulate);
	}

	public Dictionary<PositionCategory, List<Item>> GetAllEquipments() {

		return new Dictionary<PositionCategory, List<Item>> () { {
				PositionCategory.MainHand, getMainHandItems ()
			}, {
				PositionCategory.OffHand, getOffHandItems ()
			}, {
				PositionCategory.Body, getBodyItems ()
			}, {
				PositionCategory.Amulate, getAmulateItems ()
			},
		};
	}

	List<CardType> getEquipmentCards(PositionCategory category) {
		var items = this.GetEquipments (category);

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
}

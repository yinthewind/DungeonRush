using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemsScene : MonoBehaviour {

	public GameStatsPersistor GameStats;
	BackpackRenderer backpackRenderer;
	Dictionary<PositionCategory, EquipmentRenderer> equipmentRenderers;
	public ItemFactory ItemFactory;

	void Start () {

		#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor ();
		#endif

		this.ItemFactory = new ItemFactory ();
		this.GameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor").GetComponent<GameStatsPersistor> ();

		this.backpackRenderer = GameObject.Find ("Backpack").GetComponent<BackpackRenderer> ();

		equipmentRenderers = new Dictionary<PositionCategory, EquipmentRenderer> () { 
			{ PositionCategory.Amulate, new EquipmentRenderer("Amulate") }, 
			{ PositionCategory.Body, new EquipmentRenderer("Body") },
			{ PositionCategory.MainHand, new EquipmentRenderer("MainHand") },
			{ PositionCategory.OffHand, new EquipmentRenderer("OffHand") },
		};

		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.IronSword));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.IronSword));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.WoodenBow));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Sapphire));

		foreach (var item in this.GameStats.PlayerItemStats.GetItems()) {

			render (item.Pos);

			item.OnMouseDrop = (Vector3 posV) => {


				var thisPos = item.Pos;
				var thatPos = getPosition(posV);
				if (thatPos.Category == PositionCategory.Nowhere) {
					this.GameStats.PlayerItemStats.Put(thisPos, item);
					render(thisPos);
					return ;
				}
				var success = this.GameStats.PlayerItemStats.Put(thatPos, item);

				render(thisPos);
				render(thatPos);
			};
		}
	}

	void render(Position pos) {
		var item = this.GameStats.PlayerItemStats.GetItem (pos);
		if (item == null) {
			return;
		}
		if (pos.Category == PositionCategory.Backpack) {
			this.backpackRenderer.Render (pos, item);
		} else {
			var render = this.equipmentRenderers [pos.Category];
			render.Render (pos, item);
		}
	}

	Position getPosition(Vector3 posV) {
		if (this.backpackRenderer.Inside (posV)) {
			var index = this.backpackRenderer.GetIndex (posV);
			return new Position (PositionCategory.Backpack, index);
		}
		foreach (var kv in equipmentRenderers) {
			var cat = kv.Key;
			var render = kv.Value;
			if (render.Inside(posV)) {
				var index = render.GetIndex (posV);
				return new Position(cat, index);
			}
		}
		return new Position(PositionCategory.Nowhere, 0);
	}
}

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
				PositionCategory.Amulate, dummyChecker
			}, { 
				PositionCategory.Backpack, dummyChecker
			}, { 
				PositionCategory.Body, dummyChecker 
			}, {
				PositionCategory.MainHand, dummyChecker
			}, {
				PositionCategory.OffHand, dummyChecker
			},
		};
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
}
﻿using System;
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

	public Item GetWeapon() {
		return this.GetItem (new Position (PositionCategory.MainHand, 0));
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

	public List<CardType> GetDeck() {

		var deck = new List<CardType> ();

		var mainHandCards = getMainHandCards ();
		var offHandCards = getOffHandCards ();
		var armorCards = getArmorCards ();
		var amulateCards = getAmulateCards ();

		deck.AddRange (mainHandCards);
		deck.AddRange (offHandCards);
		deck.AddRange (armorCards);
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

	List<Item> getMainHandItems() {

		var mainHandPos = new Position (PositionCategory.MainHand, 0);

		var result = new List<Item> ();

		for(int i = 0;i < 5;i++) {
			mainHandPos.Index = i;
			if(this.GetItem(mainHandPos) != null) {
				result.Add (this.items [mainHandPos]);
			}
		}
		return result; 
	}

	List<CardType> getMainHandCards() {

		var items = this.getMainHandItems ();

		if (items.Count == 0 || items[0] ==  null) {
			return mainHandDefaultCards;
		}
		return items [0].Cards;
	}

	List<CardType> getOffHandCards() {
		var cards = new List<CardType> ();
		return cards;
	}

	List<CardType> getArmorCards() {
		var cards = new List<CardType> ();
		return cards;
	}

	List<CardType> getAmulateCards() {
		var cards = new List<CardType> ();
		return cards;
	}
}
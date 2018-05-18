using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsScene : MonoBehaviour {

	public GameStatsPersistor GameStats;
	public Backpack Backpack;
	// 0: main, 1: off, 2: body, 3: amulate
	public List<Slot> Slots = new List<Slot> ();

	void Start () {

#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor ();
#endif

		this.GameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor").GetComponent<GameStatsPersistor> ();

		this.Backpack = GameObject.Find ("Backpack").GetComponent<Backpack> ();

		var MainHandSlot = GameObject.Find ("MainHandSlot").GetComponent<Slot> ();
		var OffHandSlot = GameObject.Find ("OffHandSlot").GetComponent<Slot> ();
		var BodySlot = GameObject.Find ("BodySlot").GetComponent<Slot> ();
		var AmulateSlot = GameObject.Find ("AmulateSlot").GetComponent<Slot> ();
		this.Slots.Add (MainHandSlot);
		this.Slots.Add (OffHandSlot);
		this.Slots.Add (BodySlot);
		this.Slots.Add (AmulateSlot);

		this.GameStats.PlayerItemStats.AddItem (new MagicSquare ());
		this.GameStats.PlayerItemStats.AddItem (new MagicHex ());
		this.GameStats.PlayerItemStats.AddItem (new MagicCircle ());

		foreach (var item in this.GameStats.PlayerItemStats.Items) {
			if (item.BackpackIndex == -1) {
				var idx = this.Backpack.NextAvailableIndex ();
				this.Backpack.ClaimIndex (idx, item.Index);
				item.BackpackIndex = idx;
			}
			var pos = this.Backpack.GetPosition (item.BackpackIndex);
			item.Render (pos);

			// TODO: don't return position, just an event handler
			item.OnMouseDrop = (Vector3 p) => {

				var slotIndex = insideSlot (p);

				if (item.Slot == -1) { // Not equipped
					if (slotIndex != -1) { // Dropped in one of slots

						var bIndex = item.BackpackIndex;
						takeFromBackpack (item);

						if (this.GameStats.PlayerItemStats.Slots [slotIndex] != null) {

							var otherItem = takeFromSlot (slotIndex);
							putIntoBackpack (bIndex, otherItem);
						}
						putIntoSlot (slotIndex, item);
				
					} else if (this.Backpack.Inside (p)) { // To Backpack
						var bIndex = this.Backpack.GetIndex (p);
						if (!putIntoBackpack (bIndex, item)) { // Backpack grid occupied  
							swapInBackpack (bIndex, item.BackpackIndex);
						} 
					} else { // Don't move
						item.MoveTo(this.Backpack.GetPosition (item.BackpackIndex));
					}
				} else { // Equipped

					if (slotIndex != -1) { // Dropped in one of slots

						var thisSlot = item.Slot;
						var thisItem = takeFromSlot(item.Slot);

						if (this.GameStats.PlayerItemStats.Slots[slotIndex] != null) { // other slot is occupied
							var otherItem = takeFromSlot(slotIndex);
							putIntoSlot(thisSlot, otherItem);
						}
						putIntoSlot(slotIndex, thisItem);
						
					} else if (this.Backpack.Inside(p)) { // To Backpack
						
						var bIndex = this.Backpack.GetIndex(p);
						if (putIntoBackpack(bIndex, item)) {
							takeFromSlot(item.Slot);
						} else {
							item.MoveTo(this.Slots[item.Slot].GetPosition());
						}

					} else {
						item.MoveTo(this.Slots[item.Slot].GetPosition());
					}
				}
			};
		}
	}

	// Return true on a success put
	bool putIntoSlot(int slotIndex, Item item) {
		item.Slot = slotIndex;
		this.GameStats.PlayerItemStats.Slots [slotIndex] = item;
		item.MoveTo (this.Slots [slotIndex].GetPosition ());

		return true;
	}

	Item takeFromSlot(int slotIndex) {
		
		var item = this.GameStats.PlayerItemStats.Slots [slotIndex];
		this.GameStats.PlayerItemStats.Slots [slotIndex] = null;
		item.Slot = -1;

		return item;
	}
	
	int insideSlot(Vector3 pos) {

		for(int i = 0; i < this.Slots.Count; i++) {
			if (this.Slots [i].Inside (pos)) {
				return i;
			}
		}
		return -1;
	}

	// try to put into backpack, return true if succeed
	bool putIntoBackpack(int bIndex, Item item) {
		if (this.Backpack.ClaimIndex(bIndex, item.Index)) { // Backpack grid vacant
			this.Backpack.Release(item.BackpackIndex);
			item.BackpackIndex = bIndex;
			item.MoveTo (this.Backpack.GetPosition (bIndex));
			return true;
		}
		return false;
	}

	void takeFromBackpack(Item item) {

		this.Backpack.Release (item.BackpackIndex);
		item.BackpackIndex = -1;
	}

	void swapInBackpack(int bIndex1, int bIndex2) {
		
		var thisItem = this.Backpack.GetItem (bIndex1);
		var otherItem = this.Backpack.GetItem (bIndex2);

		// update item status
		this.GameStats.PlayerItemStats.Items [thisItem].BackpackIndex = bIndex2;
		this.GameStats.PlayerItemStats.Items [otherItem].BackpackIndex = bIndex1;

		// update graphics
		this.GameStats.PlayerItemStats.Items [thisItem].MoveTo (this.Backpack.GetPosition (bIndex2));
		this.GameStats.PlayerItemStats.Items [otherItem].MoveTo (this.Backpack.GetPosition (bIndex1));

		// update backpack status
		this.Backpack.Release (bIndex1);
		this.Backpack.Release (bIndex2);
		this.Backpack.ClaimIndex (bIndex2, thisItem);
		this.Backpack.ClaimIndex (bIndex1, otherItem);
	}
}

public class ItemStats {
	public List<Item> Items = new List<Item>();
	public Item MainHand;
	public Item OffHand;
	public Item Body;
	public Item Amulate;
	public List<Item> Slots = new List<Item>() {null, null, null, null};

	public void AddItem(Item item) {
		Items.Add (item);
		item.Index = Items.Count - 1;
	}

	public void RemoveItem(int index) {
	}
}

public class Item {
	public int Index;

	public string Name;
	public string SpriteName;

	public int Slot = -1;
	public int BackpackIndex = -1;
	public delegate void del(Vector3 pos);
	public del OnMouseDrop;
	GameObject go;

	public void Render(Vector2 pos) {

		this.go = new GameObject (this.Name);

		go.transform.position = pos;
		go.transform.localScale = new Vector2 (100, 100);

		go.AddComponent<ItemRenderer> ().Item = this;

		var sr = go.AddComponent<SpriteRenderer> ();
		sr.sprite = Resources.Load<Sprite> (SpriteName);
		sr.material.color = Color.gray;

		var collider = go.AddComponent<BoxCollider2D> ();
	}

	public void MoveTo(Vector3 pos) {
		go.transform.position = pos;
	}
}

public class ItemRenderer : MonoBehaviour {

	public Item Item;

	void OnMouseDown() {
		this.gameObject.GetComponent<SpriteRenderer> ().material.color = Color.yellow;
	}

	void OnMouseDrag() {
		var mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouseWorldPoint.z = 0;
		var newPos = mouseWorldPoint;
		transform.position = newPos;
	}

	void OnMouseUp() {
		this.gameObject.GetComponent<SpriteRenderer> ().material.color = Color.gray;

		this.Item.OnMouseDrop(this.transform.position);
	}
}

public class MagicSquare : Item {
	public MagicSquare() {
		this.Name = "MagicSquare";
		this.SpriteName = "Square";
	}
}


public class MagicHex : Item {
	public MagicHex() {
		this.Name = "MagicHex";
		this.SpriteName = "Hex";
	}
}
		

public class MagicCircle : Item {
	public MagicCircle() {
		this.Name = "MagicCircle";
		this.SpriteName = "Circle";
	}
}

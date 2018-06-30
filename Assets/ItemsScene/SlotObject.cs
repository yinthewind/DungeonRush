using UnityEngine;
using System;

public class SlotObject : MonoBehaviour {

	public Position Position;
	public Func<Item, bool> Checker;

	protected ItemObject item;

	protected Bounds bounds;
	protected Vector3 size;
	protected Vector3 leftTopPos;

	GameStats gameStats;

	void Awake() {

		var collider = this.gameObject.AddComponent<BoxCollider2D> ();
		collider.isTrigger = true;
		collider.size = bounds.size / 10;

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}

	void Start() {
		this.gameStats = tryFindGameStats();

		renderContainedItem();
	}

	void OnTriggerStay2D(Collider2D other) {
		var sr = other.gameObject.GetComponent<SpriteRenderer>();
		if(this.Inside(sr.bounds.center)) {
			other.gameObject.GetComponent<ItemObject>().Destination = this;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.GetComponent<ItemObject>().Destination == this) {
			other.gameObject.GetComponent<ItemObject>().Destination = null;
		}
	}

	void renderContainedItem() {
		if(this.gameStats == null) {
			return;
		}
		var item = this.gameStats.PlayerItemStats.GetItem(Position);
		if (item == null) {
			return;
		}
		this.item = item.InitItemObject();
		this.Render();
	}

	GameStats tryFindGameStats() {
		var gameStatsPersistorObject = GameObject.FindGameObjectWithTag ("GameStatsPersistor");
		if(gameStatsPersistorObject == null) {
			return null;
		}
		var gameStatsPersistor = gameStatsPersistorObject.GetComponent<GameStatsPersistor> ();
		if(gameStatsPersistor == null) {
			return null;
		}
		return gameStatsPersistor.GameStats;
	}

	public ItemObject Take() {
		var item = this.item;
		item.Slot = null;
		this.item = null;

		// Update GameStats.PlayerItemStats
		this.gameStats.PlayerItemStats.Take(this.Position);

		return item;
	}

	public void Render() {

		item.Slot = this;
		item.transform.position = this.bounds.center;
		var itemSize = item.GetSize();
		var scale = Math.Min(size.x / itemSize.x, size.y / itemSize.y) * 0.85f;
		item.transform.localScale = item.transform.localScale * scale;

		item.GetComponent<SpriteRenderer>().sortingOrder = 
			this.GetComponent<SpriteRenderer>().sortingOrder + 1;
	}

	public void Put(ItemObject item) {
		this.item = item;
		this.Render();

		// Update GameStats.PlayerItemStats
		this.gameStats.PlayerItemStats.Put(this.Position, item.Item);
	}

	public bool Check(ItemObject item) {
		if(this.Checker == null) {
			return true;
		}
		return this.Checker(item.Item);
	}
	
	// An item is dropped to this slot
	public bool Drop(ItemObject otherItem) {
		if (this.item!= null) {
			var otherSlot = otherItem.Slot;

			if(!this.Check(otherItem) || !otherSlot.Check(this.item)) {
				return false;
			}

			var thisItem = this.Take();
			otherSlot.Take();

			otherSlot.Put(thisItem);
			this.Put(otherItem);
		} else {

			if(!this.Check(otherItem)) {
				return false;
			}

			var otherSlot = otherItem.Slot;
			if(otherSlot != null) {
				otherSlot.Take();
			}
			this.Put(otherItem);
		}

		return true;
	}

	public bool Inside(Vector3 pos) {
		if (pos.x < this.bounds.min.x || pos.x > this.bounds.max.x) {
			return false;
		}
		if (pos.y < this.bounds.min.y || pos.y > this.bounds.max.y) {
			return false;
		}
		return true;
	}

	public Vector3 GetPosition() {
		return this.bounds.center;
	}
}

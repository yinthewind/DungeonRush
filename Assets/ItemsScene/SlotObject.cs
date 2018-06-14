using UnityEngine;
using System;

public class SlotObject : MonoBehaviour {

	public Position Position;

	protected ItemObject item;

	protected Bounds bounds;
	protected Vector3 size;
	protected Vector3 leftTopPos;

	GameStats gameStats;

	void Awake() {

		var collider = this.gameObject.AddComponent<BoxCollider2D> ();
		collider.isTrigger = true;

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}

	void Start() {
		this.gameStats = GameObject.Find("Main Camera").GetComponent<ItemsScene>().GameStats;
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

	public ItemObject Take() {
		var item = this.item;
		item.Slot = null;
		this.item = null;

		// Update GameStats.PlayerItemStats
		this.gameStats.PlayerItemStats.Take(this.Position);

		return item;
	}

	public void Render(ItemObject item) {
		this.item = item;

		item.Slot = this;
		item.transform.position = this.bounds.center;
		var itemSize = item.GetSize();
		var scale = Math.Min(size.x / itemSize.x, size.y / itemSize.y) * 0.85f;
		item.transform.localScale = item.transform.localScale * scale;
	}

	public void Put(ItemObject item) {
		this.Render(item);

		// Update GameStats.PlayerItemStats
		this.gameStats.PlayerItemStats.Put(this.Position, item.Item);
	}
	
	public bool Drop(ItemObject otherItem) {
		// Validate set operation
		if (this.item!= null) {
			var otherSlot = otherItem.Slot;
			var thisItem = this.Take();
			otherSlot.Take();

			otherSlot.Put(thisItem);
			this.Put(otherItem);
		} else {
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

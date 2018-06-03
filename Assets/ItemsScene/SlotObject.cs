using UnityEngine;

public class SlotObject : MonoBehaviour {

	protected ItemObject item;

	protected Bounds bounds;
	protected Vector3 size;
	protected Vector3 leftTopPos;

	void Start() {
		var collider = this.gameObject.AddComponent<BoxCollider2D> ();
		collider.isTrigger = true;

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}

	void OnTriggerStay2D(Collider2D other) {
		var sr = other.gameObject.GetComponent<SpriteRenderer>();
		if(this.Inside(sr.bounds.center)) {
			other.gameObject.GetComponent<ItemObject>().Destination = this;
		}
	}

	public ItemObject Take() {
		var item = this.item;
		item.Slot = null;
		this.item = null;
		return item;

		// Update GameStats.PlayerItemStats
	}

	public void Put(ItemObject item) {
		this.item = item;

		item.transform.position = this.bounds.center;
		item.Slot = this;

		// Update GameStats.PlayerItemStats
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

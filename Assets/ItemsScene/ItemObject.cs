using System;
using UnityEngine;

public class ItemObject : MonoBehaviour {

	public Item Item;
	public SlotObject Slot;

	int defaultSortingOrder = 9;
	public SlotObject Destination;

	void OnMouseDown() {
		var sr = this.gameObject.GetComponent<SpriteRenderer>();
		// before anything else
		sr.sortingOrder = 999;
	}

	void OnMouseUp() {
		var sr = this.gameObject.GetComponent<SpriteRenderer>();
		sr.sortingOrder = defaultSortingOrder;

		if(this.Destination != null && this.Destination != this.Slot) {
			this.Destination.Drop(this);
		} else {
			if(this.Slot != null) {
				this.Slot.Put(this);
			}
		}
	}

	void OnMouseDrag() {
		var mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouseWorldPoint.z = 1;
		var newPos = mouseWorldPoint;
		transform.position = newPos;
	}

	public Vector3 GetSize() {
		return this.gameObject.GetComponent<SpriteRenderer>().bounds.size;
	}
}

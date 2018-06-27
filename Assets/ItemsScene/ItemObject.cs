using System;
using UnityEngine;

public class ItemObject : MonoBehaviour {

	public Item Item;
	public SlotObject Slot;

	int defaultSortingOrder = 9;
	Color defaultColor;
	public SlotObject Destination;

	void OnMouseDown() {
		var sr = this.gameObject.GetComponent<SpriteRenderer>();
		// before anything else
		sr.sortingOrder = 999;
		this.defaultColor = sr.material.color;
		sr.material.color = Color.yellow;

		this.Item.IsDragging = true;
	}

	void OnMouseUp() {
		var sr = this.gameObject.GetComponent<SpriteRenderer>();
		sr.sortingOrder = this.defaultSortingOrder;
		sr.material.color = this.defaultColor;
		this.Item.IsDragging = false;

		if(this.Destination != null && this.Destination != this.Slot) {
			var success = this.Destination.Drop(this);
			if(!success) {
				this.Slot.Put(this);
			}
		} else if(this.Slot != null) {
			this.Slot.Put(this);
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

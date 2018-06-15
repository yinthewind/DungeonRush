using System.Collections.Generic;
using UnityEngine;

public class SlotsContainer : GridContainerRenderer {
	public PositionCategory PositionCategory;

	List<SlotObject> slots;
	GameObject slotPrefab;

	GameObject createBackpackSlot(Vector3 position) {
		return Instantiate(this.slotPrefab, position, Quaternion.identity);
	}

	public List<SlotObject> GetSlots() {
		return this.slots;
	}

	void Start () {
		var prefabPath = "Prefabs/Slot";
		this.slotPrefab = Resources.Load<GameObject>(prefabPath);

		this.slots = new List<SlotObject>();

		for(int i = 0; i < this.row * this.col; i++) {
			var pos = this.GetPosition(i);
			Debug.Log(pos + " ");
			var slot = createBackpackSlot(pos).GetComponent<SlotObject>();
			slot.Position = new Position(PositionCategory, i);
			this.slots.Add(slot);
		}

		var sr = this.gameObject.GetComponent<SpriteRenderer>();
		bounds = sr.bounds;
	}
}

using System.Collections.Generic;
using UnityEngine;

public class SlotsContainer : GridContainer {
	public PositionCategory PositionCategory;

	List<SlotObject> slots;
	GameObject slotPrefab;

	GameObject createBackpackSlot(Vector3 position) {
		return Instantiate(this.slotPrefab, position, Quaternion.identity);
	}

	public List<SlotObject> GetSlots() {
		return this.slots;
	}

	void initSlots() {
		var prefabPath = "Prefabs/Slot";
		this.slotPrefab = Resources.Load<GameObject>(prefabPath);

		this.slots = new List<SlotObject>();

		for(int i = 0; i < this.row * this.col; i++) {
			var pos = this.GetPosition(i);
			var slot = createBackpackSlot(pos).GetComponent<SlotObject>();
			slot.Position = new Position(PositionCategory, i);
			slot.GetComponent<SpriteRenderer>().sortingOrder = 3;
			this.slots.Add(slot);
		}
	}

	void Awake () {
		base.Awake();
		this.initSlots();
	}
}

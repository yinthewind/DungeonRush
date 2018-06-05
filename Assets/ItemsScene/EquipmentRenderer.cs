using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentRenderer {

	public List<SlotObject> Slots;

	public EquipmentRenderer(PositionCategory category) {
		var gameObject = GameObject.Find (category.ToString());

		this.Slots = new List<SlotObject> () { 
			gameObject.GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket0").GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket1").GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket2").GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket3").GetComponent<SlotObject> (),
		};

		for(int i = 0; i < this.Slots.Count; i++) {
			var slot = this.Slots[i];
			slot.Position = new Position(category, i);
		}
	}
}

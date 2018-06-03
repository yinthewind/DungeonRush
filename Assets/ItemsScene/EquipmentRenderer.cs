using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentRenderer {

	public SlotObject Core;
	public List<SlotObject> Sockets;

	public EquipmentRenderer(string objectName) {
		var gameObject = GameObject.Find (objectName);
		this.Core = gameObject.GetComponent<SlotObject> ();

		this.Sockets = new List<SlotObject> () { 
			gameObject.transform.Find ("Socket0").GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket1").GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket2").GetComponent<SlotObject> (),
			gameObject.transform.Find ("Socket3").GetComponent<SlotObject> (),
		};
	}
}

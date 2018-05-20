using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentRenderer {

	public ContainerRenderer Core;
	public List<ContainerRenderer> Sockets;

	public EquipmentRenderer(string objectName) {
		var gameObject = GameObject.Find (objectName);
		this.Core = gameObject.GetComponent<ContainerRenderer> ();

		this.Sockets = new List<ContainerRenderer> () { 
			gameObject.transform.Find ("Socket0").GetComponent<ContainerRenderer> (),
			gameObject.transform.Find ("Socket1").GetComponent<ContainerRenderer> (),
			gameObject.transform.Find ("Socket2").GetComponent<ContainerRenderer> (),
			gameObject.transform.Find ("Socket3").GetComponent<ContainerRenderer> (),
		};
	}

	public bool Inside(Vector3 pos) {
		var idx = this.GetIndex (pos);
		return idx != -1;
	}

	public int GetIndex(Vector3 pos) {
		if (this.Core.Inside (pos)) {
			return 0;
		}
		for (int i = 0; i < this.Sockets.Count; i++) {
			if (this.Sockets [i].Inside (pos)) {
				return i + 1;
			}
		}
		return -1;
	}

	public void Render(Position pos, Item item) {
		var index = pos.Index;
		var posV = this.getPosition (index);
		var scale = this.getScale (index);

		if (item.Renderer == null) {
			item.Render (posV);
		} else {
			item.MoveTo (posV);
		}
		item.Scale(scale);
	}

	Vector3 getPosition(int index) {
		if (index == 0) {
			return this.Core.GetPosition ();
		} else {
			return this.Sockets [index - 1].GetPosition ();
		}
	}

	float getScale(int index) {
		if (index == 0) {
			return 1.5f;
		} else {
			return 0.45f;
		}
	}
}

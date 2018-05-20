using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Slot : MonoBehaviour {

	Bounds bounds;
	Vector3 size;
	Vector3 leftTopPos;

	public List<Socket> Sockets;

	public bool Inside(Vector3 pos) {

		foreach (var socket in this.Sockets) {
			if (socket.Inside (pos)) {
				return true;
			}
		}

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

	public Vector3 GetSize() {
		return this.size;
	}

	void Start () {
		var socket0 = transform.Find ("Socket0").GetComponent<Socket> ();
		var socket1 = transform.Find ("Socket1").GetComponent<Socket> ();
		var socket2 = transform.Find ("Socket2").GetComponent<Socket> ();
		var socket3 = transform.Find ("Socket3").GetComponent<Socket> ();

		this.Sockets = new List<Socket> () { socket0, socket1, socket2, socket3 };

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}
}
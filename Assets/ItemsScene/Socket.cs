using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Socket : MonoBehaviour {

	Bounds bounds;
	Vector3 size;
	Vector3 leftTopPos;

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

	public Vector3 GetSize() {
		return this.size;
	}

	void Start () {

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}
}

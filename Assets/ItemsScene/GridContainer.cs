using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridContainer : MonoBehaviour {

	public int col = 4;
	public int row = 6;

	protected Bounds bounds;
	protected Vector3 size;
	protected Vector3 leftTopPos;

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

	public void Awake () {

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}

	int getRow(int index) {
		return index / col;
	}

	int getCol(int index) {
		return index % col;
	}

	public Vector3 GetPosition(int index) {

		int c = getCol (index);
		int r = getRow (index);

		return this.leftTopPos
			+ new Vector3 (size.x * (2 * c + 1) / (2 * col), -size.y * (2 * r + 1) / (2 * row));
	}

	public int GetIndex(Vector3 pos) {
		var d = pos - this.leftTopPos;

		int c = (int) (d.x / (size.x / col));
		int r = (int) (d.y / (-size.y / row));

		return r * col + c;
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Backpack : MonoBehaviour {

	public int col;
	public int row;
	Bounds bounds;
	Vector3 size;
	Vector3 leftTopPos;
	// what item stored in each grid?
	List<int> grids;

	void Awake() {
		col = 5;
		row = 4;

		this.grids = new List<int> (col * row);
		for (int i = 0; i < col * row; i++) {
			this.grids.Add (-1);
		}
	}

	int getRow(int index) {
		return index / col;
	}

	int getCol(int index) {
		return index % col;
	}

	public int GetItem(int index) {
		return this.grids[index];
	}

	public int NextAvailableIndex() {
		for (int i = 0; i < this.grids.Count; i++) {
			if (this.grids [i] == -1) {
				return i;
			}
		}
		return -1;
	}

	public bool ClaimIndex(int index, int item) {
		if (this.grids [index] == -1) {
			this.grids [index] = item;
			return true;
		}
		return false;
	}

	public void Release(int index) {
		if (index < 0 || index >= this.grids.Count) {
			return;
		}
		this.grids [index] = -1;
	}

	public bool Inside(Vector3 pos) {
		if (pos.x < this.bounds.min.x || pos.x > this.bounds.max.x) {
			return false;
		}
		if (pos.y < this.bounds.min.y || pos.y > this.bounds.max.y) {
			return false;
		}
		return true;
	}

	public Vector3 GetPosition(int index) {

		int c = getCol (index);
		int r = getRow (index);

		return this.leftTopPos + new Vector3 (size.x * (2 * c + 1) / (2 * col), -size.y * (2 * r + 1) / (2 * row));
	}

	public int GetIndex(Vector3 pos) {
		var d = pos - this.leftTopPos;

		int c = (int) (d.x / (size.x / col));
		int r = (int) (d.y / (-size.y / row));

		return r * col + c;
	}

	void Start () {

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
	}
}
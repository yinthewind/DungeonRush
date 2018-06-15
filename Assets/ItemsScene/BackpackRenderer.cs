using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridContainerRenderer : ContainerRenderer {
	public int col = 4;
	public int row = 6;

	int getRow(int index) {
		return index / col;
	}

	int getCol(int index) {
		return index % col;
	}

	public Vector3 GetPosition(int index) {

		int c = getCol (index);
		int r = getRow (index);
		Debug.Log(col + " " + row + " " + index);

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

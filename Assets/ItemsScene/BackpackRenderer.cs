using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridContainerRenderer : ContainerRenderer {
	protected int col = 5;
	protected int row = 4;

	int getRow(int index) {
		return index / col;
	}

	int getCol(int index) {
		return index % col;
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
}

public class BackpackRenderer : MonoBehaviour {
	public int row = 4;
	public int col = 6;

	protected Bounds bounds;
	protected Vector3 size;
	protected Vector3 leftTopPos;

	List<SlotObject> slots;
	Vector2 gridExtend;
	GameObject slotPrefab;

	int getRow(int index) {
		return index / col;
	}

	int getCol(int index) {
		return index % col;
	}

	public Vector3 GetPosition(int index) {

		int c = getCol (index);
		int r = getRow (index);

		return this.leftTopPos + new Vector3 (
			this.gridExtend.x * (2 * c + 1), -this.gridExtend.y * (2 * r + 1));
	}

	GameObject createBackpackSlot(Vector3 position) {
		return Instantiate(this.slotPrefab, position, Quaternion.identity);
	}

	public List<SlotObject> GetSlots() {
		return this.slots;
	}

	void Awake() {
		var prefabPath = "Prefabs/Slot";
		this.slotPrefab = Resources.Load<GameObject>(prefabPath);

		this.bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;
		this.size = bounds.size;
		this.leftTopPos = bounds.center - new Vector3 (size.x / 2, -size.y / 2);
		gridExtend = new Vector2(bounds.size.x / col / 2, bounds.size.y / row / 2);

		this.slots = new List<SlotObject>();

		for(int i = 0; i < this.row * this.col; i++) {
			var pos = this.GetPosition(i);
			var slot = createBackpackSlot(pos).GetComponent<SlotObject>();
			slot.Position = new Position(PositionCategory.Backpack, i);
			this.slots.Add(slot);
		}

		var sr = this.gameObject.GetComponent<SpriteRenderer>();
		bounds = sr.bounds;
	}
}

using UnityEngine;
using System.Linq;

public class Item {
	public Position Pos;

	public string Name;
	Sprite sprite;

	public ItemRenderer Renderer;
	public delegate void del(Vector3 pos);
	public del OnMouseDrop;
	GameObject go;
	Vector3 defaultScale;

	public void Init(ItemMeta meta, Sprite sprite) {
		this.sprite = sprite;
	}

	public void Render(Vector2 pos) {

		this.go = new GameObject (this.Name);

		go.transform.position = pos;
		go.transform.localScale = new Vector2 (500, 500);

		this.Renderer = go.AddComponent<ItemRenderer> ();
		this.Renderer.Item = this;

		var sr = go.AddComponent<SpriteRenderer> ();
		sr.sprite = sprite;
		sr.sortingOrder = 1;
		sr.material.color = Color.gray;

		var collider = go.AddComponent<BoxCollider2D> ();

		defaultScale = this.go.transform.localScale;
	}

	public void MoveTo(Vector3 pos) {
		go.transform.position = pos;
	}

	public void Scale(float factor) {
		go.transform.localScale = defaultScale * factor;
	}
}

public class ItemRenderer : MonoBehaviour {

	public Item Item;

	void OnMouseDown() {
		this.gameObject.GetComponent<SpriteRenderer> ().material.color = Color.yellow;
	}

	void OnMouseDrag() {
		var mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouseWorldPoint.z = 0;
		var newPos = mouseWorldPoint;
		transform.position = newPos;
	}

	void OnMouseUp() {
		this.gameObject.GetComponent<SpriteRenderer> ().material.color = Color.gray;

		this.Item.OnMouseDrop(this.transform.position);
	}
}
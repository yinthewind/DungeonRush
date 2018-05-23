using UnityEngine;
using System.Linq;

public class Item {
	public Position Pos;

	Sprite sprite;
	public string Name;
	public string Comment;
	public int SpeedBonus;

	public ItemRenderer Renderer;
	public delegate void del(Vector3 pos);
	public del OnMouseDrop;
	GameObject go;
	Vector3 defaultScale;

	public void Init(ItemMeta meta, Sprite sprite) {
		this.sprite = sprite;
		this.SpeedBonus = meta.SpeedBonus;
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
	int defaultSortingOrder;

	void OnMouseDown() {
		var sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.material.color = Color.yellow;
		this.defaultSortingOrder = sr.sortingOrder;

		// before anything else
		sr.sortingOrder = 999;
	}

	void OnMouseDrag() {
		var mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouseWorldPoint.z = 0;
		var newPos = mouseWorldPoint;
		transform.position = newPos;
	}

	void OnMouseUp() {
		var sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.material.color = Color.gray;
		sr.sortingOrder = this.defaultSortingOrder;

		this.Item.OnMouseDrop(this.transform.position);
	}
}
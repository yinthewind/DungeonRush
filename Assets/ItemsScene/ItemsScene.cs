using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsScene : MonoBehaviour {

	public GameStatsPersistor GameStats;
	public Backpack Backpack;

	void Start () {

#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor();
#endif

		this.GameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor").GetComponent<GameStatsPersistor> ();

		this.Backpack = GameObject.Find ("Backpack").GetComponent<Backpack> ();

		this.GameStats.PlayerItemStats.AddItem (new MagicSquare ());
		this.GameStats.PlayerItemStats.AddItem (new MagicHex ());
		this.GameStats.PlayerItemStats.AddItem (new MagicCircle ());

		foreach (var item in this.GameStats.PlayerItemStats.Items) {
			if (item.BackpackIndex == -1) {
				var idx = this.Backpack.NextAvailableIndex();
				this.Backpack.ClaimIndex (idx);
				item.BackpackIndex = idx;
			}
			var pos = this.Backpack.GetPosition (item.BackpackIndex);
			item.Render (pos);

			item.GetDropPosition = (Vector3 p) => {
				if(this.Backpack.Inside(p)) {
					var idx = this.Backpack.GetIndex(p);
					if(this.Backpack.ClaimIndex(idx)) {
						this.Backpack.Release(item.BackpackIndex);
						item.BackpackIndex = idx;
						return this.Backpack.GetPosition(idx);
					}
				}
				// Grid alread occupied, or item is dragged to an invalid position, put this item back to its original position
				return this.Backpack.GetPosition(item.BackpackIndex);
			};
		}
	}
}

public class ItemStats {
	public List<Item> Items = new List<Item>();
	public Item MainHand;
	public Item OffHand;
	public Item Body;
	public Item Amulate;

	public void AddItem(Item item) {
		Items.Add (item);
		item.Index = Items.Count - 1;
	}

	public void RemoveItem(int index) {
	}
}

public class Item {
	public string Name;
	public string SpriteName;
	public bool Equipped;
	public int BackpackIndex = -1;
	public int Index;
	public delegate Vector3 del(Vector3 pos);
	public del GetDropPosition;

	public void Render(Vector2 pos) {

		var go = new GameObject (this.Name);

		go.transform.position = pos;
		go.transform.localScale = new Vector2 (100, 100);

		go.AddComponent<ItemRenderer> ().Item = this;

		var sr = go.AddComponent<SpriteRenderer> ();
		sr.sprite = Resources.Load<Sprite> (SpriteName);
		sr.material.color = Color.gray;

		var collider = go.AddComponent<BoxCollider2D> ();
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

		var nextPos = this.Item.GetDropPosition(this.transform.position);
		this.transform.position = nextPos;
	}
}

public class MagicSquare : Item {
	public MagicSquare() {
		this.Name = "MagicSquare";
		this.SpriteName = "Square";
	}
}


public class MagicHex : Item {
	public MagicHex() {
		this.Name = "MagicHex";
		this.SpriteName = "Hex";
	}
}
		

public class MagicCircle : Item {
	public MagicCircle() {
		this.Name = "MagicCircle";
		this.SpriteName = "Circle";
	}
}

using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class Item {
	ItemMeta meta;

	public string Name {
		get { return meta.Name; }
	}

	public List<CardType> Cards {
		get { return meta.Cards; }
	}

	public ItemCategory Category {
		get { return meta.Category; }
	}

	public int AttackBonus {
		get { return meta.AttackBonus; }
	}

	public int DefenceBonus {
		get { return meta.DefenceBonus; }
	}

	public int SpeedBonus {
		get { return meta.SpeedBonus; }
	}

	public Position Position;
	// User is dragging this item
	public bool IsDragging = false;

	Sprite sprite;

	public void Init(ItemMeta meta, Sprite sprite) {
		this.meta = meta;
		this.sprite = sprite;
	}

	public ItemObject InitItemObject() {
		var gameObject = new GameObject(this.Name);

		var itemObject = gameObject.AddComponent<ItemObject>();
		itemObject.Item = this;

		var sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = this.sprite;
		sr.sortingOrder = 1;

		gameObject.AddComponent<BoxCollider2D>();
		gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

		return itemObject;
	}
}

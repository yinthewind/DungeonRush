using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class Item {
	public Position Position;
	// User is dragging this item
	public bool IsDragging = false;

	public ItemType Type;
	public ItemCategory Category;

	Sprite sprite;
	public string Name;
	public string Comment;
	public int AttackBonus;
	public int SpeedBonus;
	public int DefenceBonus;

	GameObject gameObject;
	Vector3 defaultScale;
	public List<CardType> Cards;

	public void Init(ItemMeta meta, Sprite sprite) {
		this.sprite = sprite;
		this.SpeedBonus = meta.SpeedBonus;
		this.Cards = meta.Cards;
		this.AttackBonus = meta.AttackBonus;
		this.DefenceBonus = meta.DefenceBonus;

		this.Category = meta.Category;
	}

	public ItemObject InitItemObject() {
		this.gameObject = new GameObject(this.Name);

		var itemObject = this.gameObject.AddComponent<ItemObject>();
		itemObject.Item = this;

		var sr = this.gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = this.sprite;
		sr.sortingOrder = 1;

		this.gameObject.AddComponent<BoxCollider2D>();
		this.gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

		return itemObject;
	}
}

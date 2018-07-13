using UnityEngine;

public class Card {
	public CardMeta Meta;

	public GameObject Instantiate(GameObject parent, Vector3 pos, Vector2 size) {
		var go = new GameObject("Card");
		//go.AddComponent<CardRenderer>();

		go.AddComponent<SpriteRenderer>();

		go.AddComponent<BoxCollider2D>();

		return go;
	}
}

using UnityEngine;

public class Card {
	public CardMeta Meta;

	public GameObject Instantiate(GameObject parent, Vector3 pos, Vector2 targetSize) {
		var go = new GameObject("Card");

		go.transform.SetParent(parent.transform, false);
		go.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(this.Meta.SpriteName);
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<CardRenderer>();

		go.transform.position = pos;
		go.GetComponent<SpriteRenderer>().sortingOrder = 1;
		var spriteSize = go.GetComponent<SpriteRenderer>().bounds.size;
		go.transform.localScale =
			new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 0);

		return go;
	}
}

public class CardRenderer : MonoBehaviour {

	public void Awake() {
	}

	public void Start() {
	}
}

using UnityEngine;

public class Card {
	public CardMeta Meta;

	public GameObject Instantiate(GameObject parent, Vector3 pos, Vector2 targetSize) {
		var go = new GameObject("Card");

		go.transform.SetParent(parent.transform, false);
		go.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(this.Meta.SpriteName);
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<CardRenderer>().Meta = Meta;

		go.transform.position = pos;
		go.GetComponent<SpriteRenderer>().sortingOrder = 1;
		var spriteSize = go.GetComponent<SpriteRenderer>().bounds.size;
		go.transform.localScale =
			new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 0);

		return go;
	}
}

public class CardRenderer : MonoBehaviour {

	TextObject cardDescription;
	public CardMeta Meta;


	public void Awake() {
	}

	public void Start() {
	}

	void OnMouseDown() {
		if (this.Meta != null) {
			foreach (var e in this.Meta.Effects) {
				var msg = e.GenerateMsg(Camera.main.GetComponent<FightScene>().Player);
				Camera.main.GetComponent<FightScene>().BroadcastToActors(msg.MethodName(), msg);
			}
		}
		GameObject.Destroy(this.gameObject);
	}

	void OnMouseOver() {
		displayCardDescription();
	}

	void OnMouseExit() {
		hideCardDescription();
	}

	void displayCardDescription() {
		if(this.cardDescription != null) {
			return;
		}

		this.cardDescription = new TextObject(this.gameObject);

		this.cardDescription.Renderer.TextComponent.text = Meta.Name + ": \n" + Meta.Comment;
		this.cardDescription.Renderer.TextComponent.color = Color.blue;
		this.cardDescription.Renderer.TextComponent.alignment = TextAnchor.MiddleCenter;
		this.cardDescription.Renderer.TextComponent.fontSize = 20;
	}

	void hideCardDescription() {
		if(this.cardDescription != null) {
			this.cardDescription.Destroy();
			this.cardDescription = null;
		}
	}
}

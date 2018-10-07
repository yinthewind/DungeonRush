using UnityEngine;

public class Card {
	public CardMeta Meta;
	GameObject gameObject;

	public void Discard() {
		if(this.gameObject != null) {
			this.gameObject.GetComponent<CardRenderer>().Discard();
		}
	}

	public GameObject Instantiate(GameObject parent, Vector3 pos, Vector2 targetSize) {
		var go = new GameObject("Card");
		this.gameObject = go;

		go.transform.SetParent(parent.transform, false);
		go.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(this.Meta.SpriteName);
		go.AddComponent<BoxCollider2D>();
		go.AddComponent<CardRenderer>().Card = this;

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
	public Card Card;
	FightScene fightScene;

	public void Awake() {
	}

	public void Start() {
		this.fightScene = Camera.main.GetComponent<FightScene>();
	}

	void OnMouseDown() {
		this.BeforeCardPlay();
		this.OnCardPlay();
		this.AfterCardPlay();

	}

	void BeforeCardPlay() {
	}

	void OnCardPlay() {
		if (this.Card.Meta != null) {
			foreach (var e in this.Card.Meta.Effects) {
				var msg = e.GenerateMsg(this.fightScene.Player);
				this.fightScene.BroadcastToActors(msg.MethodName(), msg);
			}
		}
	}

	void AfterCardPlay() {
		this.Discard();
	}

	public void Discard() {
		GameObject.Destroy(this.gameObject);
		this.fightScene.BroadcastToActors("OnCardDiscarded", this.Card);
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

		this.cardDescription.Renderer.TextComponent.text = Card.Meta.Name + ": \n" + Card.Meta.Comment;
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

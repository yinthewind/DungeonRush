using UnityEngine;
using System.Collections;

public class Card
{
	public GameObject Object;
	public CardRenderer Renderer;
	public FightScene FightScene;

	public string SpriteName;

	public delegate void OnClickEventHandler();
	public OnClickEventHandler OnClick;

	protected bool shouldExhausted = false;

	public Card() 
	{
		OnClick += Play;
	}

	public virtual void Render()
	{
		this.Object = new GameObject("CardInHand");
		this.Renderer = this.Object.AddComponent<CardRenderer>();
		this.Renderer.Card = this;
		this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(this.SpriteName);
		this.Object.AddComponent<BoxCollider2D> ();
	}

	public virtual void Play()
	{
		GameObject.Destroy (Object);
		this.FightScene.Hand.RemoveCard (this);
		this.FightScene.Hand.RenderHand ();
		if (shouldExhausted == false) {
			this.FightScene.DiscardPile.Add (this);
		}
	}

	/// <summary>
	/// This card is actively discarded by player
	/// discarded because of other cards or artifacts
	/// </summary>
	public virtual void Discard()
	{
		GameObject.Destroy (Object);
	}

	/// <summary>
	/// This card is not played by player, but discarded at the ending of a turn
	/// </summary>
	public virtual void PassiveDiscard()
	{
		GameObject.Destroy (Object);
		this.FightScene.DiscardPile.Add (this);
	}

	public virtual void Exile()
	{
		GameObject.Destroy (Object);
	}

	public virtual int GetCalculatedDamage(FightScene fightScene)
	{
		return 0;
	}
}

public class CardRenderer : MonoBehaviour
{
	protected SpriteRenderer sr;

	public Card Card;

	protected virtual void OnMouseDown()
	{
		if (Card != null && Card.OnClick != null)
		{
			Card.OnClick();
		}
	}

	public void SetPosition(Vector3 position, Vector3 scale)
	{
		this.gameObject.transform.position = position;
		this.gameObject.transform.localScale = scale;
	}
}

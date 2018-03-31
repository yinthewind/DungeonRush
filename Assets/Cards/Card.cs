using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card
{
	public GameObject Object;
	public CardRenderer Renderer;
	public FightScene FightScene;

	public string SpriteName;
	public string Name;
	public string Comment;

	public int baseDamage = 0;
	public int baseArmor = 0;
	public int energyCost = 0;

	public delegate void OnMouseActionEventHandler();
	public OnMouseActionEventHandler OnMouseDown;

	protected bool shouldExhausted = false;

	public Card(CardType cardType) 
	{
		this.OnMouseDown += Play;

		this.SpriteName = CardConfigurations.Metas [cardType].SpriteName;
		this.Name = CardConfigurations.Metas [cardType].Name;
		this.Comment = CardConfigurations.Metas [cardType].Comment;

		this.baseDamage = CardConfigurations.Metas [cardType].BaseDamage;
		this.baseArmor = CardConfigurations.Metas [cardType].BaseArmor;
		this.energyCost = CardConfigurations.Metas [cardType].EnergyCost;
	}

	public virtual void Render()
	{
		this.Object = new GameObject("CardInHand");
		this.Renderer = this.Object.AddComponent<CardRenderer>();
		this.Renderer.Card = this;
		this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(this.SpriteName);
		this.Object.AddComponent<BoxCollider2D> ();
	}

	public void Play()
	{
		if (FightScene.Player.Energy.Val < this.energyCost) {
			return;
		}
		this.BeforePlay ();
		this.OnPlay ();
		this.AfterPlay ();
	}

	public virtual void BeforePlay() 
	{
		GameObject.Destroy (Object);
		this.FightScene.Hand.RemoveCard (this);
	}

	public virtual void OnPlay()
	{
		this.FightScene.Player.Energy.Val -= this.energyCost;
		this.FightScene.Player.Shield.Val += this.baseArmor;
		this.FightScene.Monster.Hitpoint.Val -= this.GetCalculatedDamage ();
	}

	public virtual void AfterPlay() 
	{
		if (shouldExhausted == false) {
			this.FightScene.DiscardPile.Add (this);
		}
	}

	public virtual int GetCalculatedDamage()
	{
		var damageOutput = (baseDamage + this.FightScene.Player.States.AttackModifier) * this.FightScene.Player.States.DamageModifier;
		var damageTookByMonster = damageOutput * this.FightScene.Monster.States.DamageTookModifier;
		Debug.Log (baseDamage);
		Debug.Log (this.FightScene.Player.States.AttackModifier);
		Debug.Log ((int)damageOutput);
		return (int)damageTookByMonster;
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
}

public class CardRenderer : MonoBehaviour
{
	protected SpriteRenderer sr;

	public Card Card;

	GameObject cardDescriptionObject;

	protected virtual void OnMouseDown()
	{
		if (Card != null && Card.OnMouseDown != null)
		{
			Card.OnMouseDown();
		}
	}

	protected virtual void OnMouseOver()
	{
		displayCardDescription ();
	}

	protected virtual void OnMouseExit()
	{
		hideCardDescription ();
	}


	public void displayCardDescription()
	{
		if (this.cardDescriptionObject != null) {
			return;
		}

		this.gameObject.AddComponent<Canvas> ();
		this.cardDescriptionObject = new GameObject ("text");
		this.cardDescriptionObject.transform.SetParent (this.gameObject.transform);
		this.cardDescriptionObject.transform.localPosition = new Vector3 (0, 0);

		var textComponent = this.cardDescriptionObject.AddComponent<Text>();
		textComponent.text = this.Card.Name + ": " + this.Card.Comment;
		textComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		textComponent.color = Color.blue;
		textComponent.alignment = TextAnchor.MiddleCenter;
		textComponent.fontSize = 20;
	}

	public void hideCardDescription()
	{
		Destroy (this.cardDescriptionObject);
	}

	public void SetPosition(Vector3 position, Vector3 scale)
	{
		this.gameObject.transform.position = position;
		this.gameObject.transform.localScale = scale;
	}
}

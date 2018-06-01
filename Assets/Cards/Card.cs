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

	protected float damageMotifier = 1f;
	protected int baseDamage = 0;
	protected int baseArmor = 0;
	protected int energyCost = 0;

	public delegate void OnMouseActionEventHandler();
	public OnMouseActionEventHandler OnMouseDown;

	protected bool shouldExhausted = false;

	public void Init(CardMeta meta) {
		this.OnMouseDown += Play;

		this.SpriteName = meta.SpriteName;
		this.Name = meta.Name;
		this.Comment = meta.Comment;

		this.baseDamage = meta.BaseDamage;
		this.damageMotifier = meta.DamageMultifier;
		this.baseArmor = meta.BaseArmor;
		this.energyCost = meta.EnergyCost;
	}

	public void Render(GameObject container, Vector3 pos, Vector3 targetSize) {

		if (this.Object == null) {
			this.Object = new GameObject ("CardInHand");
			this.Renderer = this.Object.AddComponent<CardRenderer> ();
			this.Renderer.Card = this;

			this.Object.transform.SetParent (container.transform, false);
			this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (this.SpriteName);
			this.Object.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			this.Object.AddComponent<BoxCollider2D> ();


			var spriteSize = this.Object.GetComponent<SpriteRenderer> ().bounds.size;

			this.Object.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 0);
		}
		this.Renderer.Render (pos);
	}

	public virtual void Play()
	{
		if (FightScene.Hand.NeedDiscard > 0) {
			FightScene.Hand.NeedDiscard--;
			this.Discard ();
			return;
		}
		if (FightScene.Player.Energy.Val < this.energyCost* this.FightScene.Player.States.EnergyModifier) {
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
		this.FightScene.Player.Energy.Val -= this.energyCost * this.FightScene.Player.States.EnergyModifier;
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
		var damageOutput = (baseDamage + this.FightScene.Player.States.AttackModifier + this.FightScene.PlayerFightStats.Attack) * this.FightScene.Player.States.DamageModifier * this.damageMotifier;
		var damageTookByMonster = damageOutput * this.FightScene.Monster.States.DamageTookModifier;
		return (int)damageTookByMonster;
	}

	/// <summary>
	/// This card is actively discarded by player
	/// discarded because of other cards or artifacts
	/// </summary>
	public virtual void Discard()
	{
		GameObject.Destroy (Object);
		this.FightScene.DiscardPile.Add (this);
		this.FightScene.Hand.RemoveCard (this);
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

	TextObject cardDescription;

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
		if (this.cardDescription != null) {
			return;
		}

		this.cardDescription = new TextObject (this.gameObject);

		this.cardDescription.Renderer.TextComponent.text = this.Card.Name + ": " + this.Card.Comment;
		this.cardDescription.Renderer.TextComponent.color = Color.blue;
		this.cardDescription.Renderer.TextComponent.alignment = TextAnchor.MiddleCenter;
		this.cardDescription.Renderer.TextComponent.fontSize = 20;
	}

	public void hideCardDescription()
	{
		if (this.cardDescription != null) {
			this.cardDescription.Destroy ();
			this.cardDescription = null;
		}
	}

	public void Render(Vector3 pos) {
		this.transform.position = pos;
	}
}

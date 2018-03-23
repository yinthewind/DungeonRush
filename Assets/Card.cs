using UnityEngine;
using System.Collections;

public class Card
{
    public GameObject Object;
    public CardRenderer Renderer;
    public FightScene FightScene;

    public delegate void OnClickEventHandler();
    public OnClickEventHandler OnClick;

    public virtual void Render()
    {
        this.Object = new GameObject("CardInHand");
        this.Renderer = this.Object.AddComponent<CardRenderer>();
        this.Renderer.Card = this;

		OnClick += Play;
    }

    public virtual void Play()
    {
        GameObject.Destroy (Object);
    }

	public virtual void Discard()
	{
		GameObject.Destroy (Object);
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
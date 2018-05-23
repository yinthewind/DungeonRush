using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hand {

    GameObject handObject;

    public FightScene FightScene;
	public StatesBar States;

    int cardLimit = 10;
	int drawPerTurn;
	public int NeedDiscard = 0;

    public List<Card> cards;

    public Hand(FightScene fightScene)
    {
        this.FightScene = fightScene;

        handObject = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");
        handObject.AddComponent<HandRenderer>();

		this.States = new StatesBar (handObject);
		this.drawPerTurn = FightScene.GameStats.Speed;
    }

	public void Render()
	{
		var basePos = handObject.transform.position;
		var hScale = handObject.transform.localScale;
		var hWidth = hScale.x;
		var hHeight = hScale.y;
		basePos -= new Vector3(hWidth / 2, 0);
		var scale = new Vector3(hWidth / 64, hHeight / 8, 0);

		var handSize = handObject.GetComponent<SpriteRenderer> ().bounds.size;
		var cardHeight = handSize.y * 0.7f;
		var cardWidth = cardHeight * 0.618f;
		var cardSize = new Vector2 (cardWidth, cardHeight);

		for (int i = 0; i < cards.Count; i++) 
		{
			var pos = basePos + new Vector3(i * hWidth / 12 + hWidth / 24, 0, 1);
			cards[i].Render (handObject, pos, cardSize);
		}
	}

	public void DrawNewCard(int num)
	{
		if(this.States.AllowDraw == true){
			for (int i = 0; i < num; i++)
			{
				var newCard = this.FightScene.DrawPile.Draw();
				if (newCard == null) {
					break;
				}
				cards.Add(newCard);
				newCard.FightScene = this.FightScene;
			}
			this.Render ();
		}
	}

	public void RemoveCard(Card card)
	{
		for (int i = 0; i < cards.Count; i++) 
		{
			if (cards.ElementAt (i) == card) 
			{
				cards.Remove (card);
			}
		}
		this.Render ();
	}

    public void StartTurn()
    {
        cards = new List<Card>();
		this.States.StartTurn ();
		var numToDraw = drawPerTurn + this.States.ExtraCardsNum;
		DrawNewCard(numToDraw);

    }

    public void EndTurn()
    {
		this.States.EndTurn ();
		foreach(var card in cards)
        {
			card.PassiveDiscard ();
        }
    }
}

public class HandRenderer : MonoBehaviour
{
    GameObject placeholder;

    void Awake()
    {
        placeholder = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");
    }
}

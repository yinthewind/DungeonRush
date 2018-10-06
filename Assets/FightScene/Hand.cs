using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour {

    GameObject handObject;

    public FightScene FightScene;
	public StatesBar States;

	int drawPerTurn;
	public int NeedDiscard = 0;

    public List<Card> cards;

	public void Awake() {
        this.FightScene = Camera.main.GetComponent<FightScene>();
		this.handObject = this.gameObject;
		this.States = new StatesBar (handObject);
		this.drawPerTurn = this.FightScene.GameStats.GetSpeed();
	}

	public void DrawNewCard(int num)
	{
		if(this.States.AllowDraw == true) {
			for (int i = 0; i < num; i++)
			{
				var newCard = new Card();
				this.FightScene.BroadcastToActors("OnDrawingCard", newCard);
				if (newCard == null) {
					break;
				}
				cards.Add(newCard);
				Debug.Log("drew card" + newCard.Meta.Name);
			}
			//this.Render ();
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
		//this.Render ();
	}

	public void OnTurnStart(int turnCounter)
	{
        cards = new List<Card>();
		this.States.StartTurn ();
		var numToDraw = drawPerTurn + this.States.ExtraCardsNum;
		DrawNewCard(numToDraw);
		OnHandChanges();
	}

	public void OnTurnEnd(int turnCounter)
	{
		this.States.EndTurn ();
		foreach(var card in cards)
        {
			//card.PassiveDiscard ();
        }
    }

	public void OnHandChanges() {

		var basePos = this.transform.position;
		var hScale = this.transform.localScale;
		var hWidth = hScale.x;
		var hHeight = hScale.y;
		basePos -= new Vector3(hWidth / 2, 0);
		var scale = new Vector3(hWidth / 64, hHeight / 8, 0);

		var handSize = this.GetComponent<SpriteRenderer> ().bounds.size;
		var cardHeight = handSize.y * 0.7f;
		var cardWidth = cardHeight * 0.618f;
		var cardSize = new Vector2 (cardWidth, cardHeight);

		for (int i = 0; i < cards.Count; i++) 
		{
			var pos = basePos + new Vector3(i * hWidth / 12 + hWidth / 24, 0, 1);
			cards[i].Instantiate (this.gameObject, pos, cardSize);
		}
	}
}

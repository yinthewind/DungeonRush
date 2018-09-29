using UnityEngine;
using System;
using System.Collections.Generic;

public class DrawPile
{
	public List<Card> cards;
	GameStats gameStats;

	public DrawPile()
	{
		this.gameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor")
			.GetComponent<GameStatsPersistor> ().GameStats;
		cards = this.gameStats.GetDeck ();
		this.Shuffle ();
	}

	public void Shuffle()
	{
		for (int i = 0; i < cards.Count; i++) {
			var p = UnityEngine.Random.Range (i, cards.Count - 1);
			var temp = cards [i];
			cards [i] = cards [p];
			cards [p] = temp;
		}
	}

	public Card Draw()
	{
		Debug.Log("cards remaining: " + cards.Count);
		if (cards.Count == 0) {
			//this.cards = fightScene.DiscardPile.Get ();
			this.Shuffle ();
		}
		if (cards.Count == 0) {
			return null;
		}
		var cnt = cards.Count;
		var card = cards[cnt-1];
		cards.RemoveAt(cnt-1);

		return card;
	}
}

public class DrawPileRenderer : MonoBehaviour
{
	DrawPile drawPile;

	public void Awake() {
		this.drawPile = new DrawPile();
	}

	public void OnDrawingCard(Card card) {
		card.Meta = drawPile.Draw().Meta;
	}
}

using UnityEngine;
using System.Collections.Generic;

public class DiscardPile
{
	List<Card> cards = new List<Card>();

	public void Add(Card card)
	{
		cards.Add (card);
	}

	public List<Card> Get()
	{
		var result = cards;
		cards = new List<Card> ();
		return result;
	}
}

public class DiscardPileRenderer : MonoBehaviour
{
	DiscardPile discardPile;

	void Awake() {
		discardPile = new DiscardPile();
	}

	public void OnCardDiscarded(Card card) {
		this.discardPile.Add(card);
	}

	public void OnDrawPileEmpty(List<Card> drawPileCards) {
		drawPileCards.AddRange(this.discardPile.Get());
	}
}

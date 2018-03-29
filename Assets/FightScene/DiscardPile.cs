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

public class DicardPileRenderer : MonoBehaviour
{
}
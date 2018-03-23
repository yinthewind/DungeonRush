using UnityEngine;
using System.Collections.Generic;

public class DrawPile
{
    public List<Card> cards;

    public DrawPile(Deck deck)
    {
		cards = deck.cards;
    }

    public Card Draw()
    {
        var cnt = cards.Count;
        Debug.Log("card left in draw pile: " + cnt);
        if(cnt > 0)
        {
            var card = cards[cnt-1];
            cards.RemoveAt(cnt-1);
            return card;
        }

        return null;
    }
}

public class Deck
{
	public List<Card> cards;

	/// <summary>
	/// Create an initial deck with preset cards
	/// </summary>
	public Deck() 
	{
		cards = getInitialCards ();
	}

	List<Card> getInitialCards() {
		List<Card> deck = new List<Card> ();
		for (int i = 0; i < 5; i++) {
			deck.Add (new StrikeCard ());
		}
		for (int i = 0; i < 5; i++) {
			deck.Add (new DefendCard ());
		}
		return deck;
	}
}
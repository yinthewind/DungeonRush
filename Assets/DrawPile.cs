using UnityEngine;
using System.Collections.Generic;

public class DrawPile
{
    public List<Card> cards;
	FightScene fightScene;

    public DrawPile(FightScene fightScene)
    {
		this.fightScene = fightScene;
		cards = fightScene.GameStates.Deck.cards;
    }

    public Card Draw()
    {
		if (cards.Count == 0) {
			this.cards = fightScene.DiscardPile.Get ();
		}
		Debug.Log (this.cards.Count);
		var cnt = cards.Count;
        var card = cards[cnt-1];
        cards.RemoveAt(cnt-1);
        return card;
    }
}

public class DrawPileRenderer : MonoBehaviour
{
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
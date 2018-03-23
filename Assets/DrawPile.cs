using UnityEngine;
using System.Collections.Generic;

public class DrawPile
{
    public List<Card> cards;
	FightScene fightScene;

    public DrawPile(FightScene fightScene)
    {
		this.fightScene = fightScene;
		cards = new List<Card> (fightScene.GameStates.Deck.Get());
    }

    public Card Draw()
    {
		if (cards.Count == 0) {
			this.cards = fightScene.DiscardPile.Get ();
		}
		if (cards.Count == 0) {
			return null;
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
	List<Card> cards;

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

	public List<Card> Get() 
	{
		return new List<Card>(this.cards);
	}
}
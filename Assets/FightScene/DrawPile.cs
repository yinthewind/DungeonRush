using UnityEngine;
using System.Collections.Generic;

public class DrawPile
{
	public List<Card> cards;
	FightScene fightScene;

	public DrawPile(FightScene fightScene)
	{
		this.fightScene = fightScene;
		cards = new List<Card> (fightScene.GameStats.Deck.Get());
		this.Shuffle ();
	}

	public void Shuffle()
	{
		for (int i = 0; i < cards.Count; i++) {
			var p = Random.Range (i, cards.Count - 1);
			var temp = cards [i];
			cards [i] = cards [p];
			cards [p] = temp;
		}
	}

	public Card Draw()
	{
		if (cards.Count == 0) {
			this.cards = fightScene.DiscardPile.Get ();
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
		for (int i = 0; i < 4; i++) {
			deck.Add (new StrikeCard ());
		}
		for (int i = 0; i < 4; i++) {
			deck.Add (new DefendCard ());
		}
		for (int i = 0; i < 2; i++) {
			deck.Add (new NeutralizeCard ());
		}
		for (int i = 0; i < 2; i++) {
			deck.Add (new BreakCard ());
		}

		for (int i = 0; i < 1; i++) {
			deck.Add (new BackflipCard ());
		}
		for (int i = 0; i < 1; i++) {
			deck.Add (new AcrobaticsCard ());
		}
		for (int i = 0; i < 1; i++) {
			deck.Add (new AdrenalineCard ());
		}
		for (int i = 0; i < 1; i++) {
			deck.Add (new ReflexCard ());
		}
		for (int i = 0; i < 1; i++) {
			deck.Add (new PredatorCard ());
		}
		for (int i = 0; i < 1; i++) {
			deck.Add (new BulletCard ());
		}
		return deck;
	}

	public List<Card> Get() 
	{
		return new List<Card>(this.cards);
	}
}

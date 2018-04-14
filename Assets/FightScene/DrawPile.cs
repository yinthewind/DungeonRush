using UnityEngine;
using System;
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
			var p = UnityEngine.Random.Range (i, cards.Count - 1);
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
	public Dictionary<CardType,int> PlayerOwnedCards = new Dictionary<CardType,int>(){
		{CardType.Strike,4},
		{CardType.Defend,4},
		{CardType.Break,3},
		{CardType.Bullet,3},
		{CardType.Acrobatics,2},
		{CardType.Backflip,2},
		{CardType.Adrenaline,1},
		{CardType.Neutralize,3},
		{CardType.Dodge,2},
		{CardType.Predator,2},
		{CardType.Reflex,1},
		{CardType.Stab,1},
	};

	/// <summary>
	/// Create an initial deck with preset cards
	/// </summary>
	public Deck() 
	{
		cards = getInitialCards ();
	}

	List<Card> getInitialCards() {
		List<Card> deck = new List<Card> ();

		foreach (KeyValuePair<CardType,int> card in PlayerOwnedCards) {
			
			CardMeta cardMeta = CardConfigurations.Metas [card.Key];
			int count;

			switch (cardMeta.Rarity) {
			case CardRarity.Common:
				count = 4;
				break;
			case CardRarity.Uncommon:
				count = 3;
				break;
			case CardRarity.Rare:
				count = 2;
				break;
			case CardRarity.Mythical:
				count = 1;
				break;
			default:
				count = 1;
				break;
			}
			for (int i = 0; i < count; i++) {
				Type t = Type.GetType (cardMeta.SpriteName);
				deck.Add ((Card)Activator.CreateInstance (t));
			}
		}
		return deck;
	}

	public List<Card> Get() 
	{
		return new List<Card>(this.cards);
	}
}

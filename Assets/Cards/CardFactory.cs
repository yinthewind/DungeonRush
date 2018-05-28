using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory {
	CardConfigurations configs = new CardConfigurations();

	public Card Create(CardType cardType) {

		var meta = this.configs.Metas [cardType];
		Debug.Log ("card type: " + cardType.ToString ());
		Type t = Type.GetType (cardType.ToString());
		var card = (Card)Activator.CreateInstance (t);
		card.Init (meta);

		return card;
	}

	public List<Card> Create(List<CardType> types) {
		return types.Select (type => this.Create (type)).ToList ();
	}
}
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory {
	CardConfigurations configs = new CardConfigurations();

	public Card Create(CardType cardType) {

		var meta = this.configs.Metas [cardType];
		Type t = Type.GetType (cardType.ToString());
		var card = (Card)Activator.CreateInstance (t);
		card.Init (meta);

		return card;
	}

	public List<Card> Create(List<CardType> types) {
		if (types == null) {
			return new List<Card> ();
		}
		return types.Select (type => this.Create (type)).ToList ();
	}
}
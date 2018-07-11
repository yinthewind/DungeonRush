using UnityEngine;
using System.Collections.Generic;

public class DeckViewer : GridContainer {

	List<Card> cardsOnDisplay;

	public DeckViewer() {
		row = 3;
		col = 13;
	}

	public void RenderCards(List<Card> cards) {

		var cardSize = new Vector3 (size.y / 4, size.y / 4, 0);
		for (int i = 0; i < cards.Count; i++) {
			var pos = GetPosition (i);
			//cards [i].Render (this.gameObject, pos, cardSize);
		}
		cardsOnDisplay = cards;
	}

	public void Clear() {
		if (cardsOnDisplay == null) {
			return;
		}
		foreach (var card in cardsOnDisplay) {
			//Destroy (card.Object);
		}
	}
}

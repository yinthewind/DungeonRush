using UnityEngine;
using System.Collections.Generic;

public class DeckViewer : GridContainer {

	List<GameObject> cardObjects;

	public DeckViewer() {
		row = 3;
		col = 13;
	}

	public void RenderCards(List<Card> cards) {

		if(this.cardObjects != null) {
			this.Clear();
		}
		this.cardObjects = new List<GameObject>();

		var cardSize = new Vector3 (size.y / 4, size.y / 4, 0);
		for (int i = 0; i < cards.Count; i++) {
			var pos = GetPosition (i);
			var cardObject = cards[i].Instantiate(this.gameObject, pos, cardSize);
			this.cardObjects.Add(cardObject);
		}
	}

	public void Clear() {
		if (this.cardObjects == null) {
			return;
		}
		foreach (var cardObject in this.cardObjects) {
			Destroy (cardObject);
		}
	}
}

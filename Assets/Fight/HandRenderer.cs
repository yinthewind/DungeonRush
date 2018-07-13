using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HandRenderer : MonoBehaviour
{
    GameObject placeholder;

    void Awake()
    {
        placeholder = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");
    }

	public void OnCardChanges(List<Card> cards) {

		var basePos = this.transform.position;
		var hScale = this.transform.localScale;
		var hWidth = hScale.x;
		var hHeight = hScale.y;
		basePos -= new Vector3(hWidth / 2, 0);
		var scale = new Vector3(hWidth / 64, hHeight / 8, 0);

		var handSize = this.GetComponent<SpriteRenderer> ().bounds.size;
		var cardHeight = handSize.y * 0.7f;
		var cardWidth = cardHeight * 0.618f;
		var cardSize = new Vector2 (cardWidth, cardHeight);

		for (int i = 0; i < cards.Count; i++) 
		{
			var pos = basePos + new Vector3(i * hWidth / 12 + hWidth / 24, 0, 1);
			cards[i].Instantiate (this.placeholder, pos, cardSize);
		}
	}
}

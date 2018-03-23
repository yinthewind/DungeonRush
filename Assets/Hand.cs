using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hand {

    GameObject handObject;

    public FightScene FightScene;

    int cardLimit = 10;
    int drawPerTurn = 7;

    public List<Card> cards;

    public Hand(FightScene fightScene)
    {
        this.FightScene = fightScene;

        handObject = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");
        handObject.AddComponent<HandRenderer>();
    }

    public void StartTurn()
    {
        cards = new List<Card>();
        for (int i = 0; i < drawPerTurn; i++)
        {
			var newCard = this.FightScene.DrawPile.Draw();
            cards.Add(newCard);

            newCard.Render();
            newCard.FightScene = this.FightScene;

            var basePos = handObject.transform.position;
            var hScale = handObject.transform.localScale;
            var hWidth = hScale.x;
            var hHeight = hScale.y;

            basePos -= new Vector3(hWidth / 2, 0);

            var pos = basePos + new Vector3(i * hWidth / 12 + hWidth / 24, 0, -0.1f);
            var scale = new Vector3(hWidth / 64, hHeight / 8, 0);
            newCard.Renderer.SetPosition(pos, scale);
        }
    }

    public void EndTurn()
    {
        foreach(var card in cards)
        {
			if (card != null) {
				card.PassiveDiscard ();
			}
        }
    }
}

public class HandRenderer : MonoBehaviour
{
    GameObject placeholder;

    private void Start()
    {
        placeholder = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");
    }
}
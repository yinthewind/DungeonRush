using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hand {

    GameObject handObject;

    public FightScene FightScene;

    int cardLimit = 10;
    int drawPerTurn = 5;
	public int NeedDiscard = 0;

    public List<Card> cards;

    public Hand(FightScene fightScene)
    {
        this.FightScene = fightScene;

        handObject = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");
        handObject.AddComponent<HandRenderer>();
    }

	public void Render()
	{
		var basePos = handObject.transform.position;
		var hScale = handObject.transform.localScale;
		var hWidth = hScale.x;
		var hHeight = hScale.y;
		basePos -= new Vector3(hWidth / 2, 0);
		var scale = new Vector3(hWidth / 64, hHeight / 8, 0);

		for (int i = 0; i < cards.Count; i++) 
		{
			var pos = basePos + new Vector3(i * hWidth / 12 + hWidth / 24, 0, -0.1f);
			cards.ElementAt(i).Renderer.SetPosition(pos, scale);
		}
	}

	public void DrawNewCard(int num)
	{
		for (int i = 0; i < num; i++)
		{
			var newCard = this.FightScene.DrawPile.Draw();
			if (newCard == null) {
				break;
			}
			cards.Add(newCard);

			newCard.Render();
			newCard.FightScene = this.FightScene;
		}
		this.Render ();
	}

	public void RemoveCard(Card card)
	{
		for (int i = 0; i < cards.Count; i++) 
		{
			if (cards.ElementAt (i) == card) 
			{
				cards.Remove (card);
			}
		}
		this.Render ();
	}

    public void StartTurn()
    {
        cards = new List<Card>();
        DrawNewCard(drawPerTurn);

    }

    public void EndTurn()
    {
        foreach(var card in cards)
        {
			card.PassiveDiscard ();
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
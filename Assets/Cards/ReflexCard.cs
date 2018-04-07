using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexCard : Card {

	public ReflexCard() : base(CardType.Reflex) {
	}

	public override void Play(){
		if (FightScene.Hand.NeedDiscard > 0) {
			FightScene.Hand.NeedDiscard--;
			this.Discard ();
			return;
		}
	}

	public override void Discard(){
		this.FightScene.Hand.DrawNewCard (1);
		GameObject.Destroy (Object);
		this.FightScene.DiscardPile.Add (this);
		this.FightScene.Hand.RemoveCard (this);
	}
}
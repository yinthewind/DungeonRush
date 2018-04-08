using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorCard : Card {

	public PredatorCard() : base(CardType.Predator) {
	}

	public override void OnPlay(){
		base.OnPlay ();
		this.FightScene.Hand.States.AddState (new ExtraCardsState(1));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineCard : Card {

	public AdrenalineCard() {
		this.shouldExhausted = true;
	}

	public override void OnPlay()
	{
		base.OnPlay ();
		this.FightScene.Hand.DrawNewCard (2);
	}
}

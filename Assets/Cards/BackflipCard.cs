using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackflipCard : Card {

	public override void OnPlay(){
		base.OnPlay ();
		this.FightScene.Hand.DrawNewCard (2);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrobaticsCard : Card {

	public AcrobaticsCard(){
		this.SpriteName = "AcrobaticsCard";
	}


	public override void Play(){
		if (this.FightScene.Player.Energy.Val >= 1){

			this.FightScene.Player.Energy.Val -= 1;
			this.FightScene.Hand.DrawNewCard (3);

			// TODO: should discard one chosen card

			base.Play ();
		}
	}
}

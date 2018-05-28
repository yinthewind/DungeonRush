﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrobaticsCard : Card {

	public override void OnPlay(){
		base.OnPlay ();
		this.FightScene.Hand.DrawNewCard (3);
		this.FightScene.Hand.NeedDiscard = 1;
	}
}

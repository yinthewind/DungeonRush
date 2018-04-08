﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCard : Card {

	public BulletCard() : base(CardType.Bullet) {
	}

	public override void OnPlay(){
		base.OnPlay ();
		this.FightScene.Player.States.AddState (new EnergySaveState(0));
		this.FightScene.Hand.States.AddState (new UndrawableState (0));
	}
}
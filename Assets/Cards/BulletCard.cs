using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCard : Card {

	public override void OnPlay(){
		base.OnPlay ();
		this.FightScene.Player.States.AddState (new EnergySaveState(1));
		this.FightScene.Hand.States.AddState (new UndrawableState (1));
	}
}

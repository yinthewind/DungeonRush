using UnityEngine;

public class NeutralizeCard : Card
{

	public override void OnPlay()
	{
		base.OnPlay ();
		this.FightScene.Monster.States.AddState(new WeakState(1));
	}
}

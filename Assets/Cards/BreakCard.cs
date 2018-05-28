using UnityEngine;

public class BreakCard : Card
{
	public BreakCard() {
	}

	public override void OnPlay()
	{
		base.OnPlay ();
		this.FightScene.Monster.States.AddState (new VulnerableState (2));
	}
}

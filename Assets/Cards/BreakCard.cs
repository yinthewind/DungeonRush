using UnityEngine;

public class BreakCard : Card
{
	public BreakCard() : base(CardType.Break) {
	}

	public override void OnPlay()
	{
		base.OnPlay ();
		this.FightScene.Monster.States.AddState (new VulnerableState (2));
	}
}

using UnityEngine;

public class NeutralizeCard : Card
{
	public NeutralizeCard() : base(CardType.Neutralize) {
	}

	public override void OnPlay()
	{
		base.OnPlay ();
		this.FightScene.Monster.States.AddState(new WeakState(1));
	}
}

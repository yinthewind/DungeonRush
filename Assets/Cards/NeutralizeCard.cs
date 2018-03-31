using UnityEngine;

public class NeutralizeCard : Card
{
	int baseDamage = 3;

	public NeutralizeCard() : base(CardType.Neutralize) {
	}

	public override void Play()
	{
		this.FightScene.Monster.Hitpoint.Val -= this.baseDamage;
		this.FightScene.Monster.States.AddState(new WeakState(1));
		base.Play ();
	}
}

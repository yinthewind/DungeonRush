using UnityEngine;

public class BreakCard : Card
{
	public BreakCard() : base(CardType.Break) {
	}

	public override void Play()
	{
		if (this.FightScene.Player.Energy.Val >= 2) {

			this.FightScene.Player.Energy.Val -= 2;
			this.FightScene.Monster.Hitpoint.Val -= 8;
			this.FightScene.Monster.States.AddState (new VulnerableState (2));
			base.Play ();
		}
	}
}

using UnityEngine;

public class BreakCard : Card
{
	public BreakCard() {
		this.SpriteName = "BreakCard";
	}

	public override void Play()
	{
		if (this.FightScene.Player.Energy.Val >= 2) {

			this.FightScene.Player.Energy.Val -= 2;
			this.FightScene.Monster.HitPoint.Val -= 8;
			this.FightScene.Monster.States.AddState (new VulnerableState (1));
			base.Play ();
		}
	}
}

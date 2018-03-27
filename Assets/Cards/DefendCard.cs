using UnityEngine;

public class DefendCard : Card
{
	public DefendCard() {
		this.SpriteName = "DefendCard";
	}

	public override void Play()
	{
		if (this.FightScene.Player.Energy.Val >= 1) {
			
			this.FightScene.Player.Energy.Val -= 1;
			this.FightScene.Player.Shield.Val += 6;
			base.Play ();
		}
	}
}
using UnityEngine;

public class DefendCard : Card
{
	public DefendCard() : base(CardType.Defend) {
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
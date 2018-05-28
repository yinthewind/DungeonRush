using UnityEngine;

public class DodgeCard : Card
{

	public override void OnPlay()
	{
		float shouldDodge = Random.Range(0.0f,1.0f);
		if (shouldDodge < 0.5f) {
			base.OnPlay ();
		} 
		else {
			this.FightScene.Player.Energy.Val -= this.energyCost * this.FightScene.Player.States.EnergyModifier;
		}
	}
}

using UnityEngine;


public class StabCard : Card
{
	

	public StabCard() : base(CardType.Stab) {
	}

	public override void OnPlay()
	{
		float shouldStab = Random.Range(0.0f,1.0f);
		if (shouldStab < 0.5f) {
			base.OnPlay ();
		} 
		else {
			this.FightScene.Player.Energy.Val -= this.energyCost * this.FightScene.Player.States.EnergyModifier;
		}
	}
}

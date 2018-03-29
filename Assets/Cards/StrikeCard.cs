using UnityEngine;

public class StrikeCard : Card
{
	int baseDamage = 6;

	public StrikeCard() {
		this.SpriteName = "StrikeCard";
	}

	public override int GetCalculatedDamage(FightScene fightScene)
	{
		var damageOutput = (baseDamage + fightScene.Player.States.AttackModifier) * fightScene.Player.States.DamageModifier;
		var damageTookByMonster = damageOutput * fightScene.Monster.States.DamageTookModifier;
		return (int)damageTookByMonster;
	}

	public override void Play()
	{
		if (this.FightScene.Player.Energy.Val >= 1) {
			
			this.FightScene.Player.Energy.Val -= 1;
			this.FightScene.Monster.Hitpoint.Val -= GetCalculatedDamage(this.FightScene);
			base.Play ();
		}
	}
}
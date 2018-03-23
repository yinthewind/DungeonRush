using UnityEngine;

public class StrikeCard : Card
{
	int baseDamage = 6;

	public override void Render()
	{
		base.Render();
		this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("StrikeCard");
		this.Object.AddComponent<BoxCollider2D> ();
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
			base.Play ();
			this.FightScene.Player.Energy.Val -= 1;
			this.FightScene.Monster.HitPoint.Val -= GetCalculatedDamage(this.FightScene);
		}
	}
}
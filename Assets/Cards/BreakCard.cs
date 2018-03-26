using UnityEngine;

public class BreakCard : Card
{
	public override void Render()
	{
		base.Render();
		this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("BreakCard");
		this.Object.AddComponent<BoxCollider2D> ();
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

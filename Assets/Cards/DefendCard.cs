using UnityEngine;

public class DefendCard : Card
{
	public override void Render()
	{
		base.Render();
		this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("DefendCard");
		this.Object.AddComponent<BoxCollider2D> ();
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
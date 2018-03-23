using UnityEngine;

public class NeutralizeCard : Card
{
	public override void Render()
	{
		base.Render();
		this.Object.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("NeutralizeCard");
		this.Object.AddComponent<BoxCollider2D> ();
	}

	public override void Play()
	{
		
		this.FightScene.Monster.HitPoint.Val -= 3;
        this.FightScene.Monster.States.AddState(new WeakState(1));
		base.Play ();
	}
}
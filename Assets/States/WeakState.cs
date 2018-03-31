using UnityEngine;

public class WeakState : State
{
	public WeakState(int duration) : base(StateType.Weak, duration)
	{

	}

	public override void StartTurnEffect(StatesBar states){
		states.DamageModifier *= 0.75f;
	}

	public override void Render(GameObject container, Vector3 p, Vector3 targetSize)
	{
		base.Render (container, p, targetSize);

		this.SpriteRenderer.material.color = Color.green;
	}
}
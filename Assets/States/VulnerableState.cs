using UnityEngine;

public class VulnerableState : State
{
	public VulnerableState(int duration) : base(StateType.Vulnerable, duration)
	{

	}

	public override void StartTurnEffect(StatesBar states) {
		states.DamageTookModifier *= 1.5f;
	}

	public override void Render (GameObject container, Vector3 p, Vector3 targetSize)
	{
		base.Render (container, p, targetSize);

		this.SpriteRenderer.material.color = Color.red;
	}
}
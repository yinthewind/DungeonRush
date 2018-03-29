using UnityEngine;

public class WeakState : CharacterState
{
	public WeakState(int duration)
	{
		this.Name = "Weak";
		this.SpriteName = "WeakState";
		this.Comment = "Dealing 25% less damage";
		this.Duration = duration;
	}

	public override void StartTurnEffect(CharacterStates states){
		states.DamageModifier *= 0.75f;
	}

	public override void Render(GameObject container, Vector3 p, Vector3 targetSize)
	{
		base.Render (container, p, targetSize);

		this.SpriteRenderer.material.color = Color.green;
	}
}
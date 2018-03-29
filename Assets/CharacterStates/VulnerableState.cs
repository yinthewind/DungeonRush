using UnityEngine;

public class VulnerableState : CharacterState
{
	public VulnerableState(int duration)
	{
		this.Name = "Vulnerable";
		this.SpriteName = "VulnerableState";
		this.Comment = "Take 50% more damage from attack or spells";
		this.Duration = duration;
	}

	public override void StartTurnEffect(CharacterStates states) {
		states.DamageTookModifier *= 1.5f;
	}

	public override void Render (GameObject container, Vector3 p, Vector3 targetSize)
	{
		base.Render (container, p, targetSize);

		this.SpriteRenderer.material.color = Color.red;
	}
}
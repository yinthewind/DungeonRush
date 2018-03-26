using UnityEngine;
using System.Collections.Generic;

public class CharacterState
{
	public string Name;
	public string Comment;
	public int Duration;

	protected GameObject go;

	public virtual void TakeEffect(GameObject gObject)
	{
	}

	public virtual void EndTurnEffect (GameObject character)
	{
	}

	public virtual void StartTurnEffect(CharacterStates states)
	{
	}

	public virtual void Render(GameObject container, Vector3 p, Vector3 tSize) {
	}
}

public class WeakState : CharacterState
{
	public WeakState(int duration)
	{
		this.Name = "Weak";
		this.Comment = "Dealing 25% less damage";
		this.Duration = duration;
	}

	public override void StartTurnEffect(CharacterStates states){
		states.DamageModifier *= 0.75f;
	}

	public override void Render(GameObject container, Vector3 p, Vector3 tSize)
	{
		this.go = new GameObject (this.Name);
		this.go.transform.SetParent (container.transform, false);
		this.go.transform.position = p;

		var spriteRenderer = this.go.AddComponent<SpriteRenderer> ();
		spriteRenderer.sprite = Resources.Load<Sprite> ("WeakState");
		spriteRenderer.material.color = Color.green;

		var size = spriteRenderer.bounds.size;
		this.go.transform.localScale = new Vector3 (tSize.x / size.x, tSize.y / size.y);
	}
}

public class CharacterStates
{
	// Damage Output : (card/monster base attack + AttackModifier) * DamageModifier
	public int AttackModifier = 0;
	public float DamageModifier = 1;

	// Damage Take : Damage Output * DamageTookModifier
	public float DamageTookModifier = 1;

	// Shield Generated : (card base shield + Shield Modifier) * DefenceModifier
	public int ShieldModifier = 0;
	public float DefenceModifier = 1;

	public Dictionary<string, CharacterState> States = new Dictionary<string, CharacterState>();
	GameObject character;
	GameObject statesObject;
	CharacterStatesRenderer renderer;

	public CharacterStates(GameObject character)
	{
		this.character = character;

		this.statesObject = new GameObject("charactorStates");
        statesObject.transform.SetParent(character.transform, false);
		renderer = statesObject.AddComponent<CharacterStatesRenderer>();
	}

    public void AddState(CharacterState state)
    {
		if (this.States.ContainsKey (state.Name)) {
			this.States [state.Name].Duration += state.Duration;
		} else {
			this.States.Add (state.Name, state);
		}
		renderer.Render (this.States);
    }

	/// <summary>
	/// Compute states duration, and removed outdated states
	/// </summary>
	public void EndTurn()
	{
		foreach (var state in this.States) {
			state.Value.EndTurnEffect (character);
		}
		foreach (var pair in this.States) {
			pair.Value.Duration -= 1;
			if (pair.Value.Duration <= 0) {
				this.States.Remove (pair.Value.Name);
			}
		}
	}

	public void StartTurn()
	{
		// Reset Modifiers to default value and then reapply state effects
		AttackModifier = 1;
		DamageModifier = 1;
		DamageTookModifier = 1;
		ShieldModifier = 1;
		DefenceModifier = 1;

		foreach (var state in this.States) {
			state.Value.StartTurnEffect (this);
		}
		renderer.Render (this.States);
	}
}

public class CharacterStatesRenderer : MonoBehaviour
{
	SpriteRenderer spritRenderer;

    private void Awake()
    {
		this.spritRenderer = this.gameObject.AddComponent<SpriteRenderer> ();
		this.spritRenderer.sprite = Resources.Load<Sprite> ("Square");
		var size = this.spritRenderer.bounds.size;
		this.transform.localScale = new Vector3 (200 / size.x, 40 / size.y);

		var pSize = this.transform.parent.GetComponentInParent<SpriteRenderer> ().bounds.size;
		var pScale = this.transform.parent.GetComponentInParent<SpriteRenderer> ().transform.localScale;
		this.transform.localPosition = new Vector3 (0, -0.6f * pSize.y / pScale.y );
    }

	public void Render(Dictionary<string, CharacterState> states)
	{
		var idx = 0;
		var size = this.gameObject.GetComponent<SpriteRenderer> ().bounds.size;
		var go = this.gameObject;
		var bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;

		// use auto layout here??
		foreach (var state in states) {
			var s = new Vector3 (size.y , size.y);
			var p = bounds.min + new Vector3(0.5f * size.y, 0.5f * size.y) + new Vector3(size.y * idx, 0);
			state.Value.Render (go, p, s);
		}
	}
}
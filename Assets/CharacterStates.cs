using UnityEngine;
using System.Collections.Generic;

public class CharacterState
{
	public string Name;
	public string Comment;
	public int Duration;

	public virtual void TakeEffect(GameObject gObject)
	{
	}

	public virtual void EndTurnEffect (GameObject character)
	{
	}

	public virtual void StartTurnEffect(CharacterStates states)
	{
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

	public CharacterStates(GameObject character)
	{
		this.character = character;

        var statesObject = new GameObject("charactorStates");
        statesObject.transform.SetParent(character.transform);
        statesObject.AddComponent<CharacterStatesRenderer>();
	}

    public void AddState(CharacterState state)
    {
		if (this.States.ContainsKey (state.Name)) {
			this.States [state.Name].Duration += state.Duration;
		} else {
			this.States.Add (state.Name, state);
		}
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
	}
}

public class CharacterStatesRenderer : MonoBehaviour
{
    private void Start()
    {
    }
}
﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class State
{
	public string Name;
	public string SpriteName;
	public string Comment;
	public int Duration;
	public bool ShouldDestroy = false;

	public SpriteRenderer SpriteRenderer;

	protected GameObject go;

	public State(StateType type, int duration)
	{
		this.Duration = duration;

		this.Name = StateConfigurations.Metas [type].Name;
		this.SpriteName = StateConfigurations.Metas [type].SpriteName;
		this.Comment = StateConfigurations.Metas [type].Comment;
	}

	public virtual void TakeEffect(GameObject gObject)
	{
	}

	public virtual void EndTurnEffect (GameObject character)
	{
	}

	public virtual void ApplyEffect(StatesBar states)
	{
	}

	public virtual void Destroy()
	{
		if (this.go == null) {
			return;
		}
		GameObject.Destroy (this.go);
	}

	public virtual void Render(GameObject container, Vector3 p, Vector3 targetSize) {

		this.go = new GameObject (this.Name);
		this.go.transform.SetParent (container.transform, false);
		this.go.transform.position = p;

		this.SpriteRenderer = this.go.AddComponent<SpriteRenderer> ();
		this.SpriteRenderer.sprite = Resources.Load<Sprite> (this.SpriteName);

		var size = this.SpriteRenderer.bounds.size;
		this.go.transform.localScale = new Vector3 (targetSize.x / size.x, targetSize.y / size.y);

		this.go.AddComponent<Canvas> ();
		var textGo = new GameObject ("text");
		textGo.transform.SetParent (this.go.transform);
		textGo.transform.localPosition = new Vector3 (0, 0);

		var textComponent = textGo.AddComponent<Text>();
		textComponent.text = this.Duration.ToString ();
		textComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		textComponent.color = Color.white;
		textComponent.alignment = TextAnchor.MiddleCenter;
		textComponent.fontSize = 40;
	}
}

public class StatesBar 
{
	// Damage Output : (card/monster base attack + AttackModifier) * DamageModifier
	public int AttackModifier = 0;
	public float DamageModifier = 1;

	// Damage Take : Damage Output * DamageTookModifier
	public float DamageTookModifier = 1;

	// Shield Generated : (card base shield + Shield Modifier) * DefenceModifier
	public int ShieldModifier = 0;
	public float DefenceModifier = 1;
	public int ExtraCardsNum = 0;
	public int EnergyModifier = 1;
	public bool AllowDraw = true;

	public Dictionary<string, State> States = new Dictionary<string, State>();
	GameObject character;
	GameObject statesObject;
	StatesBarRenderer renderer;

	public StatesBar(GameObject character)
	{
		this.character = character;

		this.statesObject = new GameObject("StatesBar");
		statesObject.transform.SetParent(character.transform, false);
		renderer = statesObject.AddComponent<StatesBarRenderer>();
		renderer.Register (this);

		resetModifiers ();
	}

	public void AddState(State state)
	{
		if (this.States.ContainsKey (state.Name)) {
			this.States [state.Name].Duration += state.Duration;
		} else {
			this.States.Add (state.Name, state);
			state.ApplyEffect (this);
		}
		this.renderer.Dirty = true;
	}

	/// <summary>
	/// Compute states duration, and removed outdated states
	/// </summary>
	public void EndTurn()
	{
		foreach (var state in this.States) {
			state.Value.EndTurnEffect (character);
		}

		var statesToDelete = new List<string> ();
		foreach (var pair in this.States) {
			pair.Value.Duration -= 1;
			if (pair.Value.Duration <= 0) {
				statesToDelete.Add (pair.Value.Name);
			}
		}
		foreach (var stateName in statesToDelete) {
			this.States.Remove (stateName);
		}
		this.renderer.Dirty = true;
		this.resetModifiers ();
		this.applyStateEffects ();
	}

	public void StartTurn()
	{
	}

	void resetModifiers()
	{
		AttackModifier = 0;
		DamageModifier = 1;
		DamageTookModifier = 1;
		ShieldModifier = 0;
		DefenceModifier = 1;
		ExtraCardsNum = 0;
		EnergyModifier = 1;
		AllowDraw = true;
	}

	void applyStateEffects()
	{
		foreach (var state in this.States) {
			state.Value.ApplyEffect (this);
		}
	}
}

public class StatesBarRenderer : MonoBehaviour
{
	SpriteRenderer spritRenderer;
	StatesBar states;

	// if true, we should clean up and rerender
	public bool Dirty = true;

	private void Awake()
	{
		this.spritRenderer = this.gameObject.AddComponent<SpriteRenderer> ();
		this.spritRenderer.sprite = Resources.Load<Sprite> ("Square");
		var size = this.spritRenderer.bounds.size;
		this.transform.localScale = new Vector3 (200 / size.x, 40 / size.y);

		var pSize = this.transform.parent.GetComponentInParent<SpriteRenderer> ().bounds.size;
		var pScale = this.transform.parent.GetComponentInParent<SpriteRenderer> ().transform.localScale;
		this.transform.localPosition = new Vector3 (0, -0.6f * pSize.y / pScale.y);
	}

	public void Register(StatesBar states) {
		this.states = states;
	}

	public void Update() {
		
		if (!this.Dirty) {
			return;
		} else {
			this.Dirty = false;

			var children = this.GetComponentsInChildren<Transform> ();
			foreach (var child in children) {
				if (child == this.transform) {
					continue;
				}
				Destroy (child.gameObject);
			}
		}

		var idx = 0;
		var size = this.gameObject.GetComponent<SpriteRenderer> ().bounds.size;
		var go = this.gameObject;
		var bounds = this.gameObject.GetComponent<SpriteRenderer> ().bounds;

		// use auto layout here??
		foreach (var state in this.states.States) {

			var s = new Vector3 (size.y , size.y);
			var p = bounds.min + new Vector3(0.5f * size.y, 0.5f * size.y) + new Vector3(size.y * idx++, 0);
			state.Value.Render (go, p, s);
		}
	}
}

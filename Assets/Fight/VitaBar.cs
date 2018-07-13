using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class VitaBarRenderer : MonoBehaviour {
	Text hitpointText;
	Slider hitpointBar;
	Text shieldText;
	
	public void Awake() {
		this.hitpointText = 
			this.GetComponentsInChildren<Text> ().Single (t => t.name == "HitpointText");
		this.shieldText = 
			this.GetComponentsInChildren<Text> ().Single (t => t.name == "ArmorText");
		this.hitpointBar = 
			this.GetComponentsInChildren<Slider> ().Single (s => s.name == "HitpointBar");
	}

	public void OnHitpointChange(HitpointChangeMsg msg) {
		int oldVal = msg.OldVal;
		int newVal = msg.NewVal;
		if (newVal < oldVal) {
			new PopDropText (this.gameObject, (oldVal - newVal).ToString (), Color.red);
		}
		if (newVal < 0) {
			newVal = 0;
		}
		this.hitpointBar.value = newVal;
		updateHitpointText();
	}

	public void OnMaxHitpointChange(int newMaxHitpoint) {
		this.hitpointBar.maxValue = newMaxHitpoint;
		updateHitpointText();
	}

	void updateHitpointText() {
		this.hitpointText.text = this.hitpointBar.value.ToString() + "/" + this.hitpointBar.maxValue.ToString();
	}
}

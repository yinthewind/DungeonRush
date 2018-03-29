using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class VitaBar : MonoBehaviour
{
	Text hitpointText;
	Slider hitpointBar;
	Text shieldText;

	void Start() {

		this.hitpointText = this.GetComponentsInChildren<Text> ().Single (t => t.name == "HitpointText");
		this.shieldText = this.GetComponentsInChildren<Text> ().Single (t => t.name == "ArmorText");
		this.hitpointBar = this.GetComponentsInChildren<Slider> ().Single (s => s.name == "HitpointBar");
	}

	public void Register(MonitoredValue<int> hitpoint, int maxHitpoint, MonitoredValue<int> shield) {

		this.hitpointBar.maxValue = maxHitpoint;
		this.hitpointText.text = hitpoint.Val.ToString() + "/" + this.hitpointBar.maxValue.ToString();
		this.hitpointBar.value = hitpoint.Val;
		this.shieldText.text = shield.Val.ToString();

		hitpoint.OnChange += (oldVal, newVal) => {
			if (newVal < 0) {
				newVal = 0;
			}
			this.hitpointText.text = newVal.ToString() + "/" + this.hitpointBar.maxValue.ToString();
			this.hitpointBar.value = newVal;
		};

		shield.OnChange += (oldVal, newVal) => {
			this.shieldText.text = newVal.ToString();
		};
	}
}
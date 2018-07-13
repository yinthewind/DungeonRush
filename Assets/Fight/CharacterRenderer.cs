using UnityEngine;

public class CharacterRenderer : MonoBehaviour {

	public void Awake() {
		// Consider add Animator here
	}

	public void OnHitpointChange(HitpointChangeMsg msg) {
		int oldVal = msg.OldVal;
		int newVal = msg.NewVal;
		if (newVal < oldVal) {
			new PopDropText (this.gameObject, (oldVal - newVal).ToString (), Color.red);
		}
		this.GetComponent<Animator> ().SetTrigger ("hit");
	}
}

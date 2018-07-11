using UnityEngine;

public class Character : MonoBehaviour {
	public int MaxHitpoint;
	public int Hitpoint;

	public void Init(int maxHp, int hp) {
		this.MaxHitpoint = maxHp;
		this.Hitpoint = hp;
	}

	public void TakeDamage(int val) {
		Hitpoint -= val;
	}

	public void Start() {
		this.gameObject.SendMessage("OnMaxHitpointChange", this.MaxHitpoint);
		this.gameObject.SendMessage("OnHitpointChange", this.Hitpoint);
	}
}

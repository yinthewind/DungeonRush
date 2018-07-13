using UnityEngine;

public class HitpointChangeMsg {
	public int OldVal;
	public int NewVal;
	public HitpointChangeMsg(int oldVal, int newVal) {
		OldVal = oldVal;
		NewVal = newVal;
	}
}

public class Life : MonoBehaviour {
	public int MaxHitpoint;
	public int Hitpoint;

	public void Init(int maxHp, int hp) {
		this.MaxHitpoint = maxHp;
		this.Hitpoint = hp;
	}

	public void TakeDamage(int val) {
		this.SendMessage("OnHitpointChange", new HitpointChangeMsg(
			this.Hitpoint,
			this.Hitpoint-val
		));
		Hitpoint -= val;
	}

	public void Start() {
		this.SendMessage("OnMaxHitpointChange", this.MaxHitpoint);
		this.SendMessage("OnHitpointChange", new HitpointChangeMsg(this.Hitpoint, this.Hitpoint));
	}
}

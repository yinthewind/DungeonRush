using UnityEngine;
using System.Collections.Generic;

public class FightData {

	// We use Game Object as component containers
	public GameObject Self;
	public List<GameObject> Opponents;
}

public enum EffectType {
	ApplyDamage,
	ApplyVulnerable,
}

// Effects message that will be broadcasted
public class EffectMsg {
	// root transform of who emit this effect
	// all other objects would be the effect taker
	public GameObject Emitter;

	// broadcastMessage key
	public string MethodName() {
		return "On" + ActiveEffect.Type.ToString();
	}

	public Effect ActiveEffect;
}

// Effects should be immutable
public class Effect {
	public EffectType Type;

	public int Duration;
	public int Value;

	public EffectMsg GenerateMsg(GameObject emitter) {
		return new EffectMsg() {
			Emitter = emitter,
			ActiveEffect = this,
		};
	}
}

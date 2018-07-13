using UnityEngine;
using System.Collections.Generic;

public class FightData {

	// We use Game Object as component containers
	public GameObject Self;
	public List<GameObject> Opponents;
}

public class Effect {
}

public class DamageEffect : Effect{
	public int Damage;
}

public class VulnerableEffect : Effect{
	public int Duration;
}

public class SelfDamageEffect : Effect{
}

public class PercentageDamageEffect : Effect{
}

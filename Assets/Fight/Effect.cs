using System.Collections.Generic;

public class FightData {

	public Character Self;
	public List<Character> Opponents;
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

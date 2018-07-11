using System.Collections.Generic;

public class Card {
}

public class CardEffectConfigurations {

	public Dictionary<CardType, List<Effect>> configs = new Dictionary<CardType, List<Effect>>() {
		{
			CardType.DoubleShot, new List<Effect>() {
				new DamageEffect() { Damage = 5 },
				new DamageEffect() { Damage = 5 },
			}
		},
		{
		   CardType.Break, new List<Effect>() {
			   new VulnerableEffect() { Duration = 2 },
			   new DamageEffect() { Damage = 4 },
		   }
		}
	};
}

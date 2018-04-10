using System;
using System.Collections;
using System.Collections.Generic;

public enum CardType {
	Strike,
	Defend,
	Break,
	Bullet,
	Acrobatics,
	Backflip,
	Adrenaline,
	Neutralize,
	Bash,
	Claw,
	Dodge,
	PerfectStrike,
	Predator,
	Reflex,
	Relax,
	Slash,
	Stab,
	Stumble
}

public enum CardRarity{

	Basic,
	Common,
	Uncommon,
	Rare
}

public class CardMeta
{
	public string Name;
	public string SpriteName;
	public string Comment;
	public CardRarity Rarity;

	public int BaseDamage = 0;
	public int BaseArmor = 0;
	public int EnergyCost = 0;
}

public static class CardConfigurations
{
	public static Dictionary<CardType, CardMeta> Metas = new Dictionary<CardType, CardMeta> () { { 
			CardType.Acrobatics, new CardMeta { 
				Name = "Acrobatics", 
				SpriteName = "AcrobaticsCard", 
				Comment = "Draw 3 cards, then discard 1 card",
				Rarity = CardRarity.Common,
				EnergyCost = 1
			}
		}, { 
			CardType.Adrenaline, new CardMeta { 
				Name = "Adrenaline", 
				SpriteName = "AdrenalineCard", 
				Comment = "Draw 2 cards, gain 1 energy",
				Rarity = CardRarity.Rare,
				EnergyCost = -1
			}
		}, { 
			CardType.Backflip, new CardMeta { 
				Name = "Backflip", 
				SpriteName = "BackflipCard", 
				Comment = "Draw 2 cards, gain 5 armor",
				Rarity = CardRarity.Common,
				EnergyCost = 1
			}
		}, { 
			CardType.Break, new CardMeta { 
				Name = "Break", 
				SpriteName = "BreakCard", 
				Comment = "Deal 8 damage, apply vulnerable for 2 turns",
				Rarity = CardRarity.Common,
				BaseDamage = 8,
				EnergyCost = 2
			}
		}, { 
			CardType.Defend, new CardMeta { 
				Name = "Defend", 
				SpriteName = "DefendCard", 
				Comment = "Gain 5 armor",
				Rarity = CardRarity.Basic,
				BaseArmor = 5,
				EnergyCost = 1
			}
		}, { 
			CardType.Neutralize, new CardMeta { 
				Name = "Neutralize", 
				SpriteName = "NeutralizeCard", 
				Comment = "Deal 3 damage, apply weak for 1 turn",
				Rarity = CardRarity.Common,
				BaseDamage = 3
			}
		}, { 
			CardType.Strike, new CardMeta { 
				Name = "Strike", 
				SpriteName = "StrikeCard", 
				Comment = "Deal 6 damage",
				Rarity = CardRarity.Basic,
				BaseDamage = 6,
				EnergyCost = 1
			}
		}, {
			CardType.Bash, new CardMeta {
				Name = "Bash",
				SpriteName = "Square",
				Rarity = CardRarity.Common,
			}

		}, {
			CardType.Bullet, new CardMeta {
				Name = "Bullet",
				SpriteName = "BulletCard",
				Comment = "You cannot draw any cards this turn. Reduce the cost of cards in your hand to 0 this turn",
				Rarity = CardRarity.Rare,
				EnergyCost = 3
			}
		}, {
			CardType.Claw, new CardMeta {
				Name = "Claw",
				SpriteName = "Square",
				Rarity = CardRarity.Common,
			}
		}, {
			CardType.Dodge, new CardMeta {
				Name = "Dodge",
				SpriteName = "DodgeCard",
				Comment = "Evade attack in 50% probability",
				Rarity = CardRarity.Common,
				BaseArmor = 9999,
				EnergyCost = 1
			}
		}, {
			CardType.PerfectStrike, new CardMeta {
				Name = "PerfectStrike",
				SpriteName = "Square",
				Rarity = CardRarity.Uncommon,
			}
		}, {
			CardType.Predator, new CardMeta {
				Name = "Predator",
				SpriteName = "PredatorCard",
				Comment = "Deal 15 Damage, draw 2 more cards next turn",
				Rarity = CardRarity.Uncommon,
				BaseDamage = 15,
				EnergyCost = 2
			}
		}, {
			CardType.Reflex, new CardMeta {
				Name = "Reflex",
				SpriteName = "ReflexCard",
				Comment = "If this card is discarded from your hand, draw 1 card; unplayable",
				Rarity = CardRarity.Uncommon,
				EnergyCost = 0
			}

		}, {
			CardType.Relax, new CardMeta {
				Name = "Relax",
				SpriteName = "Square",
				Rarity = CardRarity.Common,
			}
		}, {
			CardType.Slash, new CardMeta {
				Name = "Slash",
				SpriteName = "Square",
				Rarity = CardRarity.Common,
			}
		}, {
			CardType.Stab, new CardMeta {
				Name = "Stab",
				SpriteName = "StabCard",
				Comment = "deal 12 Damage in 50% probability",
				Rarity = CardRarity.Common,
				BaseDamage = 12,
				EnergyCost = 1
			}
		}, {
			CardType.Stumble, new CardMeta {
				Name = "Stumble",
				SpriteName = "Square",
				Rarity = CardRarity.Common,

			}
		}
	};
}

using System;
using System.Collections;
using System.Collections.Generic;

public enum CardType {
	Strike,
	Defend,
	Break,
	Acrobatics,
	Backflip,
	Adrenaline,
	Neutralize,
}
	
public class CardMeta
{
	public string Name;
	public string SpriteName;
	public string Comment;
}

public static class CardConfigurations
{
	public static Dictionary<CardType, CardMeta> Metas = new Dictionary<CardType, CardMeta> () { { 
			CardType.Acrobatics, new CardMeta { 
				Name = "Acrobatics", 
				SpriteName = "AcrobaticsCard", 
				Comment = "Draw 3 cards, then discard 1 card"
			}
		}, { 
			CardType.Adrenaline, new CardMeta { 
				Name = "Adrenaline", 
				SpriteName = "AdrenalineCard", 
				Comment = "Draw 2 cards, gain 1 energy"
			}
		}, { 
			CardType.Backflip, new CardMeta { 
				Name = "Backflip", 
				SpriteName = "BackflipCard", 
				Comment = "Draw 2 cards, gain 5 armor"
			}
		}, { 
			CardType.Break, new CardMeta { 
				Name = "Break", 
				SpriteName = "BreakCard", 
				Comment = "Deal 8 damage, apply vulnerable for 2 turns"
			}
		}, { 
			CardType.Defend, new CardMeta { 
				Name = "Defend", 
				SpriteName = "DefendCard", 
				Comment = "Gain 5 armor"
			}
		}, { 
			CardType.Neutralize, new CardMeta { 
				Name = "Neutralize", 
				SpriteName = "NeutralizeCard", 
				Comment = "Deal 3 damage, apply weak for 1 turn"
			}
		}, { 
			CardType.Strike, new CardMeta { 
				Name = "Strike", 
				SpriteName = "StrikeCard", 
				Comment = "Deal 6 damage"
			}
		}
	};
}

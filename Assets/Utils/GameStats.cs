using System.Collections.Generic;
using System.Linq;

// Scenes talking to each other only by passing around GameStats through GameStatsPersistor
public class GameStats {
	public int MaxHitpoint;
	public int Hitpoint;
	public int Level;
	public int baseSpeed;
	public int baseDefence;
	public int baseAttack;

	public ItemStats PlayerItemStats;
	public DungeonMapData DungeonMap;
}

public static class GameStatsExtensions {
	public static ItemFactory ItemFactory;
	//public static CardFactory CardFactory;

	static GameStatsExtensions() {
		//CardFactory = new CardFactory ();
	}

	public static List<Card> GetDeck(this GameStats gameStats) {
		var result = new List<Card> ();

		var items = gameStats.PlayerItemStats.GetAllEquipments ().Values.SelectMany (x => x);

		foreach (var item in items) {
			var cardTypes = item.Cards;
			//var cards;
			//var cards = CardFactory.Create (cardTypes);
			/*
			if (item.IsDragging) {
				foreach (var card in cards) {
					card.IsActive = true;
				}
			}
			result.AddRange (cards);
			*/
		}

		return result;
	}

	public static int GetAttack(this GameStats gameStats) {
		var attack = gameStats.baseAttack;
		var items = gameStats.PlayerItemStats.GetAllEquipments ().Values.SelectMany(x => x);
		foreach (var item in items) {
			attack += item.AttackBonus;
		}
		return attack;
	}

	public static int GetDefence(this GameStats gameStats) {
		var defence = gameStats.baseDefence;
		var armor = gameStats.PlayerItemStats.GetArmor ();
		if (armor != null) {
			return defence += armor.DefenceBonus;
		}
		return defence;
	}

	public static int GetSpeed(this GameStats gameStats) {
		var speed = gameStats.baseSpeed;

		var weapon = gameStats.PlayerItemStats.GetWeapon ();
		if (weapon != null) {
			speed += weapon.SpeedBonus;
		}

		var amulate = gameStats.PlayerItemStats.GetAmulate ();
		if (amulate != null) {
			speed += amulate.SpeedBonus;
		}
		return speed;
	}

}

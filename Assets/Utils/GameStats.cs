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
	public static CardFactory CardFactory;

	static GameStatsExtensions() {
		CardFactory = new CardFactory ();
	}

	public static List<Card> GetDeck(this GameStats gameStats) {
		var result = new List<Card> ();

		var items = gameStats.PlayerItemStats.GetAllEquipments ().Values.SelectMany (x => x);

		foreach (var item in items) {
			var cardTypes = item.Cards;
			var cards = CardFactory.Create (cardTypes);
			if (item.IsDragging) {
				foreach (var card in cards) {
					card.IsActive = true;
				}
			}
			result.AddRange (cards);
		}

		return result;
	}
}

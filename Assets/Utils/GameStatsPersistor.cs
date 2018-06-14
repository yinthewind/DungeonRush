using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameStatsPersistor : MonoBehaviour
{
	public GameStats GameStats;

	public ItemFactory ItemFactory;
	public CardFactory CardFactory;

	public void Awake()
	{
		this.GameStats = new GameStats() {
			MaxHitpoint = 250,
			Level = 1,
			DungeonMap = new DungeonMapData(),
			PlayerItemStats = new ItemStats(),
			baseSpeed = 5,
		};

		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 0), ItemType.WoodenBow);
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 1), ItemType.WoodenBow);
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 8), ItemType.IronSword);
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 9), ItemType.Ruby);
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 10), ItemType.Ruby);
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 11), ItemType.Ruby);
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 18), ItemType.SpeedAmulate);

		this.CardFactory = new CardFactory ();
		// Create a new map.
		this.GameStats.DungeonMap = new DungeonMapData();
		Debug.Log("create a new dungeon map");

		DontDestroyOnLoad(transform.gameObject);

		this.GameStats.Hitpoint = this.GameStats.MaxHitpoint;
	}
}

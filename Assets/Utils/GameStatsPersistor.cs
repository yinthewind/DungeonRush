using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameStatsPersistor : MonoBehaviour
{
	public GameStats GameStats;

	public ItemFactory ItemFactory;

	public void Start()
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
		this.GameStats.PlayerItemStats
			.Add(new Position(PositionCategory.Backpack, 12), ItemType.IronKnife);

		var eventHub = GameObject.FindWithTag("EventHub").GetComponent<EventHub>();

		eventHub.Register(EventKey.EquipItem, "ItemSetter", (oo) => {
			var msg = (EquipItemMsg)oo;
			this.GameStats.PlayerItemStats.Put(msg.Position, msg.Item);
			return 0;
		});

		eventHub.Register(EventKey.UnequipItem, "ItemTaker", (oo) => {
			var msg = (EquipItemMsg)oo;
			this.GameStats.PlayerItemStats.Take(msg.Position);
			return 0;
		});

		// Create a new map.
		this.GameStats.DungeonMap = new DungeonMapData();
		Debug.Log("create a new dungeon map");

		DontDestroyOnLoad(transform.gameObject);

		this.GameStats.Hitpoint = this.GameStats.MaxHitpoint;
	}
}

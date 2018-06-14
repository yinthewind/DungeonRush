using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FightStats
{
	public int Level;
	public int MaxHitpoint;
	public int Hitpoint;
	public int Speed;
	public int Attack;
	public int Defence;
	public List<Card> Deck;
}

public class GameStatsPersistor : MonoBehaviour
{
	public GameStats GameStats;

	public ItemFactory ItemFactory;
	public CardFactory CardFactory;

	public FightStats PlayerFightStats {
		get {
			var stats = new FightStats() {
				Level = this.GameStats.Level,
				MaxHitpoint = this.GameStats.MaxHitpoint,
				Hitpoint = this.GameStats.Hitpoint,
				Speed = this.speed,
				Defence = this.defence,
				Deck = this.GameStats.GetDeck(),
				Attack = this.attack,
			};
			return stats;
		}
	}

	public int Hitpoint {
		get {
			return this.GameStats.Hitpoint;
		}
		set {
			this.GameStats.Hitpoint = value;
		}
	}

	public int Level {
		get {
			return this.GameStats.Level;
		}
		set {
			this.GameStats.Level = value;
		}
	}

	public DungeonMapData DungeonMap {
		get {
			return this.GameStats.DungeonMap;
		}
	}

	int speed {
		get { 
			var speed = this.GameStats.baseSpeed;

			var weapon = this.GameStats.PlayerItemStats.GetWeapon ();
			if (weapon != null) {
				speed += weapon.SpeedBonus;
			}

			var amulate = this.GameStats.PlayerItemStats.GetAmulate ();
			if (amulate != null) {
				speed += amulate.SpeedBonus;
			}
			return speed;
		}
	}

	int attack {
		get {
			var attack = this.GameStats.baseAttack;
			var items = this.GameStats.PlayerItemStats.GetAllEquipments ().Values.SelectMany(x => x);
			foreach (var item in items) {
				attack += item.AttackBonus;
			}
			return attack;
		}
	}

	int defence {
		get {
			var defence = this.GameStats.baseDefence;
			var armor = this.GameStats.PlayerItemStats.GetArmor ();
			if (armor != null) {
				return defence += armor.DefenceBonus;
			}
			return defence;
		}
	}

	public List<Card> GetDeck() {
		return this.GameStats.GetDeck();
	}

	public void Awake()
	{
		this.GameStats = new GameStats() {
			MaxHitpoint = 250,
			Level = 1,
			DungeonMap = new DungeonMapData(),
			PlayerItemStats = new ItemStats(),
			baseSpeed = 5,
		};

		this.GameStats.PlayerItemStats.Add(new Position(PositionCategory.Backpack, 0), ItemType.WoodenBow);
		this.GameStats.PlayerItemStats.Add(new Position(PositionCategory.Backpack, 1), ItemType.WoodenBow);
		this.GameStats.PlayerItemStats.Add(new Position(PositionCategory.Backpack, 2), ItemType.WoodenBow);

		this.CardFactory = new CardFactory ();
		// Create a new map.
		this.GameStats.DungeonMap = new DungeonMapData();
		Debug.Log("create a new dungeon map");

		DontDestroyOnLoad(transform.gameObject);

		this.GameStats.Hitpoint = this.GameStats.MaxHitpoint;
	}
}

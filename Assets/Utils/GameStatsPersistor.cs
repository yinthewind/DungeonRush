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
	public int MaxHitpoint = 250;
	public int Hitpoint;
	public int Level = 1;
	int baseSpeed = 5;
	int baseDefence = 0;
	int baseAttack = 0;
	public ItemStats PlayerItemStats = new ItemStats();
	public ItemFactory ItemFactory;
	public CardFactory CardFactory;

	public FightStats PlayerFightStats {
		get {
			var stats = new FightStats() {
				Level = this.Level,
				MaxHitpoint = this.MaxHitpoint,
				Hitpoint = this.Hitpoint,
				Speed = this.speed,
				Defence = this.defence,
				Deck = this.GetDeck(),
				Attack = this.attack,
			};
			return stats;
		}
	}

	int speed {
		get { 
			var speed = this.baseSpeed;

			var weapon = PlayerItemStats.GetWeapon ();
			if (weapon != null) {
				speed += weapon.SpeedBonus;
			}

			var amulate = PlayerItemStats.GetAmulate ();
			if (amulate != null) {
				speed += amulate.SpeedBonus;
			}
			return speed;
		}
	}

	int attack {
		get {
			var attack = baseAttack;
			var items = PlayerItemStats.GetAllEquipments ().Values.SelectMany(x => x);
			foreach (var item in items) {
				attack += item.AttackBonus;
			}
			return attack;
		}
	}

	int defence {
		get {
			var defence = baseDefence;
			var armor = PlayerItemStats.GetArmor ();
			if (armor != null) {
				return defence += armor.DefenceBonus;
			}
			return defence;
		}
	}

	public List<Card> GetDeck() {
		var result = new List<Card> ();

		var items = this.PlayerItemStats.GetAllEquipments ().Values.SelectMany (x => x);

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

	public void Awake()
	{
		this.ItemFactory = new ItemFactory ();
		this.CardFactory = new CardFactory ();

		DontDestroyOnLoad(transform.gameObject);

		this.Hitpoint = this.MaxHitpoint;
	}
}

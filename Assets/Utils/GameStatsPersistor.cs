using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameStatsPersistor : MonoBehaviour
{
	public int MaxHitpoint = 250;
	public int Hitpoint;
	public int Level = 1;
	int speed = 5;
	public ItemStats PlayerItemStats = new ItemStats();
	public ItemFactory ItemFactory;
	public CardFactory CardFactory;

	public int Speed {
		get { 
			var baseSpeed = speed;

			var weapon = PlayerItemStats.GetWeapon ();
			if (weapon != null) {
				baseSpeed += weapon.SpeedBonus;
			}
			return baseSpeed;
		}
	}

	public List<Card> GetDeck() {
		var cardTypes = this.PlayerItemStats.GetDeck ();
		return CardFactory.Create (cardTypes);
	}

	public void Awake()
	{
		this.ItemFactory = new ItemFactory ();
		this.CardFactory = new CardFactory ();

		DontDestroyOnLoad(transform.gameObject);

		this.Hitpoint = this.MaxHitpoint;
	}
}

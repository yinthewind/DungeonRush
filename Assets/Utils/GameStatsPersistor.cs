using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameStatsPersistor : MonoBehaviour
{
	public int MaxHitpoint = 250;
	public int Hitpoint;
	public int Level = 1;
	int speed = 5;
	public Deck Deck = new Deck();
	public ItemStats PlayerItemStats = new ItemStats();

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

	public void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);

		this.Hitpoint = this.MaxHitpoint;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameStatsPersistor : MonoBehaviour
{
	public int MaxHitpoint = 250;
	public int Hitpoint;
	public int Level = 1;
	public Deck Deck = new Deck();
	public ItemStats PlayerItemStats = new ItemStats();

	public void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);

		this.Hitpoint = this.MaxHitpoint;
	}
}

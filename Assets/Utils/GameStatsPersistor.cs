using UnityEngine;
using System.Collections;

public class GameStatsPersistor : MonoBehaviour
{
	public int MaxHitpoint = 250;
	public int Hitpoint;
	public int Level = 1;
	public Deck Deck = new Deck();

	public void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);

		this.Hitpoint = this.MaxHitpoint;
	}
}

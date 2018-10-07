using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class FightScene : MonoBehaviour
{
	public GameStatsPersistor GameStatsPersistor;
	public GameStats GameStats;

	public List<GameObject> actors;
	public GameObject Player;
	public GameObject Monster;
	public GameObject Hand;
	//public FightReport FightReport;
	//public TopMenuBar TopMenuBar;
	public GameObject DiscardPile;
	public GameObject DrawPile;

	Button endTurnButton;

	// Use this for initialization
	void Start()
	{
#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor();
#endif

		this.GameStatsPersistor = GameObject.FindGameObjectWithTag ("GameStatsPersistor")
			.GetComponent<GameStatsPersistor> ();
		this.GameStats = this.GameStatsPersistor.GameStats;

		initUIElements();

		startFight ();
	}

	void initUIElements()
	{
		var canvas = GameObject.FindObjectsOfType<Canvas>().Single(c => c.name == "FightScene");
		endTurnButton = canvas.GetComponentInChildren<Button>();
		endTurnButton.onClick.AddListener(() => {
			this.endButtonClicked = true;
		});

		//this.FightReport = new FightReport(this);
		//this.TopMenuBar = new TopMenuBar(this.GameStats.Level);
	}

	void startFight()
	{
		this.Player = newPlayer();
		this.Monster = newMonster();
		this.Hand = newHand();
		this.DrawPile = newDrawPile();
		this.DiscardPile = newDiscardPile();

		this.actors = new List<GameObject> {
			this.Player,
			this.Monster,
			this.Hand,
			this.DrawPile,
			this.DiscardPile,
		};
		/*
		this.Player.Hitpoint.OnChange += (oldVal, newVal) =>
		{
			if (newVal <= 0)
			{
				onDefeat();
			}
		};
		this.Monster = newMonster();
		this.Monster.Hitpoint.OnChange += (oldVal, newVal) =>
		{
			if (newVal <= 0)
			{
				onVictory();
			}
		};
		*/

		StartCoroutine(turnCycle());
	}

	bool endButtonClicked = false;
	int turnCounter = 1;

	public void BroadcastToActors(string methodName, object message = null) {
		foreach(var o in this.actors) {
			o.BroadcastMessage(methodName, message, SendMessageOptions.DontRequireReceiver);
		}
	}

	IEnumerator turnCycle() {
		while(true) {
			BroadcastToActors("OnTurnStart", turnCounter);

			yield return new WaitUntil(() => endButtonClicked);
			endButtonClicked = false;

			BroadcastToActors("OnTurnEnd", turnCounter++);
			yield return new WaitForEndOfFrame();
		}
	}

	private void startTurn()
	{
		/*
		this.Player.StartTurn();
		this.Hand.StartTurn();
		*/
	}

	private void clearTurn()
	{
	}

	private void endTurn()
	{
		/*
		this.Player.EndTurn();
		this.Hand.EndTurn();

		this.Monster.StartTurn ();
		this.Monster.TakeAction();
		this.Monster.EndTurn ();
		*/
	}

	void onVictory()
	{
		/*
		this.FightReport.DeclareVictory();
		this.Hand.EndTurn();
		this.Player.EndFight();
		*/

		endTurnButton.enabled = false;
	}

	void onDefeat()
	{
		/*
		this.FightReport.DeclareDefeat();
		this.Hand.EndTurn();
		*/

		endTurnButton.enabled = false;
	}

	GameObject newPlayer()
	{
		this.Player=
			GameObject.FindGameObjectsWithTag ("Placeholder").Single (o => o.name == "Player");
		this.Player.AddComponent<Life>().Init(250, 200);
		this.Player.AddComponent<CharacterRenderer>();
		this.Player.AddComponent<VitaBarRenderer>();
		// Status & Status Bar

		return Player;
	}

	GameObject newMonster()
	{
		var monster = 
			GameObject.FindGameObjectsWithTag ("Placeholder").Single (o => o.name == "Monster");
		monster.AddComponent<Life>().Init(100, 100);
		monster.AddComponent<CharacterRenderer>();
		monster.AddComponent<VitaBarRenderer>();
		// Status & Status Bar

		return monster;
	}

	GameObject newHand()
	{
		var hand = 
			GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Hand");

		hand.AddComponent<HandScript>();
		//hand.AddComponent<HandRenderer>();
		return hand;
	}

	GameObject newDrawPile()
	{
		var drawPile = new GameObject("DrawPile");
		drawPile.AddComponent<DrawPileRenderer>();

		return drawPile;
	}

	GameObject newDiscardPile()
	{
		var discardPile = new GameObject("DiscardPile");
		discardPile.AddComponent<DiscardPileRenderer>();

		return discardPile;
	}
}

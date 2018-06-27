using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class FightScene : MonoBehaviour
{
	public GameStatsPersistor GameStatsPersistor;
	public GameStats GameStats;

	public Player Player;
	public Monster Monster;
	public Hand Hand;
	public FightReport FightReport;
	public TopMenuBar TopMenuBar;
	public DiscardPile DiscardPile = new DiscardPile ();
	public DrawPile DrawPile;

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

		this.FightReport = new FightReport(this);
		this.TopMenuBar = new TopMenuBar(this.GameStats.Level);
	}

	void startFight()
	{
		this.DrawPile = new DrawPile(this);
		this.Player = newPlayer();
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
		this.Hand = newHand();

		StartCoroutine(turnCycle());
	}

	bool endButtonClicked = false;

	IEnumerator turnCycle() {
		while(true) {
			this.startTurn();

			yield return new WaitUntil(() => endButtonClicked);
			endButtonClicked = false;

			endTurn();
			yield return new WaitForEndOfFrame();
		}
	}

	private void startTurn()
	{
		this.Player.StartTurn();
		this.Hand.StartTurn();
	}

	private void clearTurn()
	{
	}

	private void endTurn()
	{
		this.Player.EndTurn();
		this.Hand.EndTurn();

		this.Monster.StartTurn ();
		this.Monster.TakeAction();
		this.Monster.EndTurn ();
	}

	void onVictory()
	{
		this.FightReport.DeclareVictory();
		this.Hand.EndTurn();
		this.Player.EndFight();

		endTurnButton.enabled = false;
	}

	void onDefeat()
	{
		this.FightReport.DeclareDefeat();
		this.Hand.EndTurn();

		endTurnButton.enabled = false;
	}

	Player newPlayer()
	{
		var player = new Player(this);
		return player;
	}

	Monster newMonster()
	{
		var monster = new Monster(this);
		return monster;
	}

	Hand newHand()
	{
		var hand = new Hand(this);
		return hand;
	}
}

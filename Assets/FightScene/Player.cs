using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Player
{
    GameObject playerObject;
    PlayerRenderer playerRenderer;
	GameStatsPersistor gameStatesPersistor;

	public int MaxHitpoint;
    public MonitoredValue<int> Hitpoint = new MonitoredValue<int>();
    public MonitoredValue<int> Energy = new MonitoredValue<int>();
	public MonitoredValue<int> Shield = new MonitoredValue<int>();

	public StatesBar States;

    public int EnergyPerTurn = 3;

    public Player()
    {
		this.gameStatesPersistor = GameObject.FindGameObjectWithTag ("GameStatsPersistor").GetComponent<GameStatsPersistor> ();
		// init for Fight Scene
		this.Hitpoint.Val = this.gameStatesPersistor.Hitpoint;
		this.MaxHitpoint = this.gameStatesPersistor.MaxHitpoint;
		this.Shield.Val = 0;

        this.Hitpoint.OnChange += (oldVal, newVal) =>
        {
            if (newVal <= 0)
            {
                onDeath();
            }
        };

		this.playerObject = GameObject.FindGameObjectsWithTag ("Placeholder").Single (o => o.name == "Player");
        this.playerRenderer = playerObject.AddComponent<PlayerRenderer>();
        this.playerRenderer.Register(this);

		this.States = new StatesBar (playerObject);
    }

    void onDeath()
    {

    }

	public void TakeDamage(int damage) 
	{
		damage = (int) (this.States.DamageTookModifier * damage);

		if (this.Shield.Val >= damage) {
			this.Shield.Val -= damage;
		} else {
			Debug.Assert (this.Shield.Val >= 0);
			damage -= this.Shield.Val;
			this.Shield.Val = 0;
			this.Hitpoint.Val -= damage;
		}
	}

    public void StartTurn()
    {
        this.Energy.Val = EnergyPerTurn;
		this.Shield.Val = 0;
		this.States.StartTurn ();
    }

    public void EndTurn()
    {
    }

    public void EndFight()
	{
		gameStatesPersistor.Hitpoint = this.Hitpoint.Val;
		gameStatesPersistor.Level++;
	}
}

/// <summary>
/// Used to render player charactor and play animation in fight scene
/// </summary>
public class PlayerRenderer : MonoBehaviour
{
    private Text energyText;

    public void Register(Player player)
    {
        var canvas = GameObject.FindObjectsOfType<Canvas>().Single(c => c.name == "FightScene");
        energyText = canvas.transform.Find("energy").GetComponent<Text>();

		player.Energy.OnChange += (oldVal, newVal) => {
			if (newVal < 0) {
				newVal = 0;
			}
			energyText.text = "Energy: " + newVal;
		};

		this.transform.Find ("VitaBar").GetComponent<VitaBar> ().Register (player.Hitpoint, player.MaxHitpoint, player.Shield);
    }
}
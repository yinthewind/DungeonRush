using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Player
{
    GameObject playerObject;
    PlayerRenderer playerRenderer;

    public MonitoredValue<int> HitPoint = new MonitoredValue<int>();
    public MonitoredValue<int> Energy = new MonitoredValue<int>();
	public MonitoredValue<int> Shield = new MonitoredValue<int>();

	public CharacterStates States;

    public int EnergyPerTurn = 3;

    public Player()
    {
        this.HitPoint.OnChange += (oldVal, newVal) =>
        {
            if (newVal <= 0)
            {
                onDeath();
            }
        };

		this.playerObject = GameObject.FindGameObjectsWithTag ("Placeholder").Single (o => o.name == "Player");
        this.playerRenderer = playerObject.AddComponent<PlayerRenderer>();
        this.playerRenderer.Register(this);

        // init for Fight Scene
        this.HitPoint.Val = GameObject.FindGameObjectWithTag("GameStatesPersistor").GetComponent<GameStatesPersistor>().HitPoint;
		this.Shield.Val = 0;

		this.States = new CharacterStates (playerObject);
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
			this.HitPoint.Val -= damage;
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
		GameObject.FindGameObjectWithTag ("GameStatesPersistor").GetComponent<GameStatesPersistor> ().HitPoint = this.HitPoint.Val;
		GameObject.FindGameObjectWithTag ("GameStatesPersistor").GetComponent<GameStatesPersistor> ().Level++;

	}
}

/// <summary>
/// Used to render player charactor and play animation in fight scene
/// </summary>
public class PlayerRenderer : MonoBehaviour
{
    private Text energyText;
    private Text hitPointText;
	private Text shieldText;

    private void Start()
    {
    }

    public void Register(Player player)
    {
        var canvas = GameObject.FindObjectsOfType<Canvas>().Single(c => c.name == "FightScene");
        energyText = canvas.transform.Find("energy").GetComponent<Text>();
        hitPointText = canvas.transform.Find("playerHp").GetComponent<Text>();
		shieldText = canvas.transform.Find ("playerShield").GetComponent<Text> ();

		player.Energy.OnChange += (oldVal, newVal) => {
			if (newVal < 0) {
				newVal = 0;
			}
			energyText.text = "Energy: " + newVal;
		};

		player.HitPoint.OnChange += (oldVal, newVal) => {
			if (newVal < 0) {
				newVal = 0;
			}
			hitPointText.text = "HP: " + newVal;
		};

		player.Shield.OnChange += (oldVal, newVal) => {
			shieldText.text = "Sheild: " + newVal;
		};
    }
}
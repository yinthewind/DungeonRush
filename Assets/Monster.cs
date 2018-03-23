using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Monster
{
    public MonitoredValue<int> HitPoint = new MonitoredValue<int>();
    FightScene fightScene;

	public CharacterStates States;

    public Monster(FightScene fightScene)
    {
        this.fightScene = fightScene;

        GameObject gObject = new GameObject();
        gObject.name = "Monster";
        gObject.AddComponent<MonsterRenderer>().Register(this);

        HitPoint.OnChange += (oldVal, newVal) =>
        {
            if(newVal <= 0)
            {
                onDeath();
            }
        };

        HitPoint.Val = 5;

		this.States = new CharacterStates (gObject);
    }

	public void StartTurn()
	{
		this.States.StartTurn ();
	}

    private void onDeath()
    {

    }

    public void TakeAction()
    {
        var damage = (int)Random.Range(5, 15);
		fightScene.Player.TakeDamage (damage);
    }
}

public class MonsterRenderer : MonoBehaviour
{
    Text enemyHpText;
    GameObject placeholder;

    private void Start()
    {
        placeholder = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Monster");
    }

    public void Register(Monster monster)
    {
        var canvas = GameObject.FindObjectsOfType<Canvas>().Single(c => c.name == "FightScene");
        enemyHpText = canvas.transform.Find("enemy").GetComponent<Text>();

        monster.HitPoint.OnChange += (oldVal, newVal) =>
        {
            if(newVal < 0)
            {
                newVal = 0;
            }
            enemyHpText.text = "Enemy HP: " + newVal;
        };
    }
}
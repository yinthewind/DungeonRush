using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Monster
{
	public MonitoredValue<int> Hitpoint = new MonitoredValue<int> ();
	public MonitoredValue<int> Shield = new MonitoredValue<int> ();
    FightScene fightScene;

	public CharacterStates States;

    public Monster(FightScene fightScene)
    {
		Hitpoint.Val = 25;

        this.fightScene = fightScene;

		GameObject gObject = GameObject.FindGameObjectsWithTag("Placeholder").Single(o => o.name == "Monster");
        gObject.AddComponent<MonsterRenderer>().Register(this);

        Hitpoint.OnChange += (oldVal, newVal) =>
        {
            if(newVal <= 0)
            {
                onDeath();
            }
        };

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
	public void EndTurn()
	{
		this.States.EndTurn ();
	}
}

public class MonsterRenderer : MonoBehaviour
{
    public void Register(Monster monster)
    {
		this.transform.Find ("VitaBar").GetComponent<VitaBar> ().Register (monster.Hitpoint, monster.Hitpoint.Val, monster.Shield);
    }
}
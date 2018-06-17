using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class FightReport
{
	GameObject rewardsWindow;
	GameObject rewardsWindowPrefab;
	Vector3 rewardsWindowPosition;
	FightScene fightScene;


	public FightReport(FightScene fightScene)
	{
		this.rewardsWindowPrefab = Resources.Load<GameObject>("Prefabs/RewardsWindow");
		this.rewardsWindowPosition = new Vector3(621, 390);
		this.fightScene = fightScene;
	}

	public void DeclareVictory()
	{
		this.rewardsWindow = GameObject.Instantiate(
			this.rewardsWindowPrefab, this.rewardsWindowPosition, Quaternion.identity);

		var persistor = this.fightScene.GameStatsPersistor;
		var stats = this.fightScene.GameStats;

		var oldRewardsToDelete = stats.PlayerItemStats.Items()
			.Where(x => x.Key.Category == PositionCategory.Rewards)
			.Select(x => x.Key).ToList();
		foreach(var key in oldRewardsToDelete) {
			stats.PlayerItemStats.Items().Remove(key);
		}
		stats.PlayerItemStats.Add(new Position(PositionCategory.Rewards, 0), ItemType.Ruby);

		this.rewardsWindow.transform.Find("Canvas")
			.Find("LeaveButton")
			.GetComponent<Button>().onClick.AddListener(victoryOnClick);

		this.rewardsWindow.transform.Find("Canvas")
			.Find("TakeAllButton")
			.GetComponent<Button>().onClick.AddListener(()=>{
				var rewards = stats.PlayerItemStats.Items()
					.Where(x => x.Key.Category == PositionCategory.Rewards)
					.Select(x => x.Value)
					.ToList();

				foreach(var reward in rewards) {
					stats.PlayerItemStats.AddItemToBackpack(reward);
				}

				victoryOnClick();
		});
	}

	public void DeclareDefeat() {
		SceneManager.LoadScene("mainMenu");
	}

	void victoryOnClick() {
		SceneManager.LoadScene("mapScene");
	}
}

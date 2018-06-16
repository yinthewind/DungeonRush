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


	public FightReport()
	{
		this.rewardsWindowPrefab = Resources.Load<GameObject>("Prefabs/RewardsWindow");
		this.rewardsWindowPosition = new Vector3(621, 390);
	}

	public void DeclareVictory()
	{
		this.rewardsWindow = GameObject.Instantiate(
			this.rewardsWindowPrefab, this.rewardsWindowPosition, Quaternion.identity);

		this.rewardsWindow.transform.Find("Close")
			.GetComponent<Button>().onClick.AddListener(victoryOnClick);
	}

	public void DeclareDefeat() {
		SceneManager.LoadScene("mainMenu");
	}

	void victoryOnClick() {
		SceneManager.LoadScene("mapScene");
	}
}

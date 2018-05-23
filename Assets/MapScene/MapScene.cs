using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class MapScene : MonoBehaviour {

	void Start() {

		GameObject.Find ("FightButton").GetComponent<Button> ().onClick.AddListener (onFightButtonClick);
		GameObject.Find ("ItemButton").GetComponent<Button> ().onClick.AddListener (onItemButtonClick);
	}

	void onFightButtonClick() {
		SceneManager.LoadScene ("fight");
	}

	void onItemButtonClick() {
		SceneManager.LoadScene ("Items");
	}
}
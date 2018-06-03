using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ItemsScene : MonoBehaviour {

	public GameStatsPersistor GameStats;
	BackpackRenderer backpackRenderer;
	Dictionary<PositionCategory, EquipmentRenderer> equipmentRenderers;
	public ItemFactory ItemFactory;
	public DeckViewer deckViewer;

	void Start () {

		#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor ();
		#endif

		GameObject.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
			SceneManager.LoadScene("mapScene");
		});
		this.ItemFactory = new ItemFactory ();
		this.GameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor")
			.GetComponent<GameStatsPersistor> ();

		this.deckViewer = GameObject.Find ("DeckViewer").GetComponent<DeckViewer> ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ItemsScene : MonoBehaviour {

	public GameStatsPersistor GameStats;
	Dictionary<PositionCategory, EquipmentRenderer> equipmentRenderers;
	BackpackRenderer backpackRenderer;
	DeckViewer deckViewer;
	Dictionary<Position, SlotObject> positionToSlot;

	void Start () {

		#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor ();
		#endif

		GameObject.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
			SceneManager.LoadScene("mapScene");
		});
		this.GameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor")
			.GetComponent<GameStatsPersistor> ();

		this.deckViewer = GameObject.Find ("DeckViewer").GetComponent<DeckViewer> ();
		this.equipmentRenderers = new Dictionary<PositionCategory, EquipmentRenderer>() { {
			PositionCategory.MainHand, new EquipmentRenderer(PositionCategory.MainHand)
		}, {
			PositionCategory.OffHand, new EquipmentRenderer(PositionCategory.OffHand)
		}};
		this.backpackRenderer = GameObject.Find("Backpack").GetComponent<BackpackRenderer>();

		this.positionToSlot = new Dictionary<Position, SlotObject>() {};

		foreach(var slot in this.backpackRenderer.GetSlots()) {
			this.positionToSlot.Add(slot.Position, slot);
		}

		foreach(var item in this.GameStats.PlayerItemStats.GetItems()) {
			var pos = item.Position;
			var itemObject = item.InitItemObject();
			this.positionToSlot[pos].Put(itemObject);
		}
	}
}

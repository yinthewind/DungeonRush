using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class ItemsScene : MonoBehaviour {

	public GameStats GameStats;
	public GameStatsPersistor GameStatsPersistor;
	Dictionary<PositionCategory, EquipmentRenderer> equipmentRenderers;
	SlotsContainer backpackRenderer;
	DeckViewer deckViewer;
	// Don't really need this..
	// A bit hacky now..
	Dictionary<Position, SlotObject> positionToSlot;

	void Awake() {

		#if UNITY_EDITOR
		DebugHelper.CreateGameStatsPersistor ();
		#endif

		this.GameStatsPersistor = GameObject.FindGameObjectWithTag ("GameStatsPersistor")
			.GetComponent<GameStatsPersistor> ();
		this.GameStats = this.GameStatsPersistor.GameStats;

		GameObject.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
			SceneManager.LoadScene("mapScene");
		});
	}

	void Start () {

		this.deckViewer = GameObject.Find ("DeckViewer").GetComponent<DeckViewer> ();
		this.equipmentRenderers = new Dictionary<PositionCategory, EquipmentRenderer>() { {
			PositionCategory.MainHand, new EquipmentRenderer(PositionCategory.MainHand)
		}, {
			PositionCategory.OffHand, new EquipmentRenderer(PositionCategory.OffHand)
		}, {
			PositionCategory.Body, new EquipmentRenderer(PositionCategory.Body)
		}, {
			PositionCategory.Amulate, new EquipmentRenderer(PositionCategory.Amulate)
		} };
		this.backpackRenderer = GameObject.Find("Backpack").GetComponent<SlotsContainer>();

		this.positionToSlot = new Dictionary<Position, SlotObject>() {};

		foreach(var slot in this.backpackRenderer.GetSlots()) {
			this.positionToSlot.Add(slot.Position, slot);
		}
		foreach(var er in this.equipmentRenderers.Select(x => x.Value)) {
			foreach(var slot in er.Slots) {
				this.positionToSlot.Add(slot.Position, slot);
			}
		}

		foreach(var item in this.GameStats.PlayerItemStats.GetItems()) {
			var pos = item.Position;
			var itemObject = item.InitItemObject();
			this.positionToSlot[pos].Render(itemObject);
		}
	}

	void Update() {

		var deck = this.GameStats.GetDeck();
		this.deckViewer.Clear();
		this.deckViewer.RenderCards(deck);
	}
}

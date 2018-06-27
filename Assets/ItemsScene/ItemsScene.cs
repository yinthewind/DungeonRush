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
	DeckViewer deckViewer;

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
			PositionCategory.MainHand, new EquipmentRenderer(PositionCategory.MainHand, 
				new Func<Item, bool>[5] {
					(item) => {
						return item.Category == ItemCategory.Weapon
							|| item.Category == ItemCategory.Shield;
					},
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
				}
			)
		}, {
			PositionCategory.OffHand, new EquipmentRenderer(PositionCategory.OffHand,
				new Func<Item, bool>[5] {
					(item) => {
						return item.Category == ItemCategory.Weapon
							|| item.Category == ItemCategory.Shield;
					},
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
				}
			)
		}, {
			PositionCategory.Body, new EquipmentRenderer(PositionCategory.Body,
				new Func<Item, bool>[5] {
					(item) => item.Category == ItemCategory.Armor,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
				}
			)
		}, {
			PositionCategory.Amulate, new EquipmentRenderer(PositionCategory.Amulate,
				new Func<Item, bool>[5] {
					(item) => item.Category == ItemCategory.Amulate,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
					(item) => item.Category == ItemCategory.Gem,
				}
			)
		} };
	}

	void Update() {

		var deck = this.GameStats.GetDeck();
		this.deckViewer.Clear();
		this.deckViewer.RenderCards(deck);
	}
}

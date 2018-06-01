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
		this.GameStats = GameObject.FindGameObjectWithTag ("GameStatsPersistor").GetComponent<GameStatsPersistor> ();

		this.backpackRenderer = GameObject.Find ("Backpack").GetComponent<BackpackRenderer> ();
		this.deckViewer = GameObject.Find ("DeckViewer").GetComponent<DeckViewer> ();

		equipmentRenderers = new Dictionary<PositionCategory, EquipmentRenderer> () { 
			{ PositionCategory.Amulate, new EquipmentRenderer("Amulate") }, 
			{ PositionCategory.Body, new EquipmentRenderer("Body") },
			{ PositionCategory.MainHand, new EquipmentRenderer("MainHand") },
			{ PositionCategory.OffHand, new EquipmentRenderer("OffHand") },
		};

		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.IronSword));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.WoodenBow));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Sapphire));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.ChainMail));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.SpeedAmulate));

		foreach (var item in this.GameStats.PlayerItemStats.GetItems()) {

			render (item.Pos);

			item.OnMouseDrop = (Vector3 posV) => {


				var thisPos = item.Pos;
				var thatPos = getPosition(posV);
				if (thatPos.Category == PositionCategory.Nowhere) {
					render(thisPos);
					return ;
				}
				var success = this.GameStats.PlayerItemStats.Put(thatPos, item);

				if (success) {
					foreach(var category in equipmentRenderers.Keys) {
						var pos = new Position(category, 0);
						if(!thisPos.Equals(pos)) {
							continue;
						}
						// If core equipment is removed, remove items in its sockets as well
						var items = this.GameStats.PlayerItemStats.GetEquipments(category);
						foreach(var x in items) {
							this.GameStats.PlayerItemStats.Take(x.Pos);
							render(x.Pos);
							this.GameStats.PlayerItemStats.AddToBackpack(x);
							render(x.Pos);
						}
					}
					deckViewerIsDirty = true;
				}

				render(thisPos);
				render(thatPos);
			};
		}
	}

	bool deckViewerIsDirty = true;

	void Update() {
		if (deckViewerIsDirty) {
			deckViewerIsDirty = false;
			var deck = this.GameStats.GetDeck ();
			this.deckViewer.Clear ();
			this.deckViewer.RenderCards (deck);
		}
	}

	void render(Position pos) {
		var item = this.GameStats.PlayerItemStats.GetItem (pos);
		if (item == null) {
			return;
		}
		if (pos.Category == PositionCategory.Backpack) {
			this.backpackRenderer.Render (pos, item);
		} else {
			var render = this.equipmentRenderers [pos.Category];
			render.Render (pos, item);
		}
	}

	Position getPosition(Vector3 posV) {
		if (this.backpackRenderer.Inside (posV)) {
			var index = this.backpackRenderer.GetIndex (posV);
			return new Position (PositionCategory.Backpack, index);
		}
		foreach (var kv in equipmentRenderers) {
			var cat = kv.Key;
			var render = kv.Value;
			if (render.Inside(posV)) {
				var index = render.GetIndex (posV);
				return new Position(cat, index);
			}
		}
		return new Position(PositionCategory.Nowhere, 0);
	}
}

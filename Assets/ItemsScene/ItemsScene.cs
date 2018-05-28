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

		equipmentRenderers = new Dictionary<PositionCategory, EquipmentRenderer> () { 
			{ PositionCategory.Amulate, new EquipmentRenderer("Amulate") }, 
			{ PositionCategory.Body, new EquipmentRenderer("Body") },
			{ PositionCategory.MainHand, new EquipmentRenderer("MainHand") },
			{ PositionCategory.OffHand, new EquipmentRenderer("OffHand") },
		};

		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.IronSword));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.IronSword));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create(ItemType.WoodenBow));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Ruby));
		this.GameStats.PlayerItemStats.AddToBackpack (ItemFactory.Create (ItemType.Sapphire));

		foreach (var item in this.GameStats.PlayerItemStats.GetItems()) {

			render (item.Pos);

			item.OnMouseDrop = (Vector3 posV) => {


				var thisPos = item.Pos;
				var thatPos = getPosition(posV);
				if (thatPos.Category == PositionCategory.Nowhere) {
					this.GameStats.PlayerItemStats.Put(thisPos, item);
					render(thisPos);
					return ;
				}
				var success = this.GameStats.PlayerItemStats.Put(thatPos, item);

				render(thisPos);
				render(thatPos);
			};
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

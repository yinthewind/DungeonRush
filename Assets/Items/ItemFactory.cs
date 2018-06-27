using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ItemFactory {

	ItemConfigurations configs = new ItemConfigurations ();
	Sprite[] sprites;

	public ItemFactory() {
		this.sprites = Resources.LoadAll<Sprite> ("roguelikeitems");
	}

	public Item Create(ItemType itemType) {

		var item = new Item();

		var meta = this.configs.ItemMetas [itemType];
		var sprite = sprites.Single (x => x.name == meta.SpriteName);
		item.Init (meta, sprite);

		return item;
	}

	public List<Item> Create(List<ItemType> itemTypes) {
		return itemTypes.Select (itemType => this.Create (itemType)).ToList();
	}
}

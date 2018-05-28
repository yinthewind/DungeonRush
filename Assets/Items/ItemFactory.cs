using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ItemFactory {

	ItemConfigurations configs = new ItemConfigurations ();
	Dictionary<ItemType, Func<Item>> creators;
	Sprite[] sprites;

	public ItemFactory() {
		this.sprites = Resources.LoadAll<Sprite> ("roguelikeitems");
	}

	public Item Create(ItemType itemType) {

		var meta = this.configs.ItemMetas [itemType];
		Type t = Type.GetType (itemType.ToString());
		var item = (Item)Activator.CreateInstance (t);

		var sprite = sprites.Single (x => x.name == meta.SpriteName);
		item.Init (meta, sprite);

		return item;
	}

	public List<Item> Create(List<ItemType> itemTypes) {
		return itemTypes.Select (itemType => this.Create (itemType)).ToList();
	}
}
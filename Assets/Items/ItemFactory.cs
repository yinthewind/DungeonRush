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
		this.creators = initCreators ();
	}

	public Item Create(ItemType type) {
		var item = creators [type] ();
		var meta = configs.ItemMetas [type];
		var sprite = sprites.Single (x => x.name == meta.SpriteName);
		item.Init (configs.ItemMetas [type], sprite);

		return item;
	}

	Dictionary<ItemType, Func<Item>> initCreators() {
		return new Dictionary<ItemType, Func<Item>> () { { 
				ItemType.IronSword,
				() => {
					return new IronSword();
				}
			}, {
				ItemType.WoodenBow,
				() => {
					return new WoodenBow();
				}
			}, {
				ItemType.Sapphire,
				() => {
					return new Sapphire();
				}
			}, {
				ItemType.Ruby,
				() => {
					return new Ruby();
				}
			},
		};
	}
}
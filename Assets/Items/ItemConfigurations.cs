﻿using System.Collections.Generic;

public enum ItemType {

	WoodenBow,
	IronSword,

	Sapphire,
	Ruby,

	Weapon,
	Shield,
	Armor,
	Amulate,
	Gem,
}

enum WeaponType {
	Dagger,
	Bow,
	Crossbow,
	Sword,
	GreatSword,
	Spear,
}

public class WoodenBow : Item {
}

public class IronSword : Item {
}

public class Sapphire : Item {
}

public class Ruby : Item {
} 

public class ItemMeta {
	public ItemType Type;
	int Level;

	public string Name;
	public string Comment;
	public string SpriteName;

	public int SpeedBonus;
}

public class ItemConfigurations {

	public Dictionary<ItemType, ItemMeta> ItemMetas = new Dictionary<ItemType, ItemMeta> () { { 
			ItemType.WoodenBow, new ItemMeta () {
				Name = "Wooden Bow",
				SpriteName = "roguelikeitems_87",
				Comment = "Common stuff",

				SpeedBonus = 1,
			}
		}, {
			ItemType.IronSword, new ItemMeta () {
				Name = "Iron Sword",
				SpriteName = "roguelikeitems_71",
				Comment = "Casual stuff",

				SpeedBonus = 0,
			}
		}, {
			ItemType.Sapphire, new ItemMeta() {
				Name = "Sapphire",
				SpriteName = "roguelikeitems_29",
				Comment = "Ice cold",
			}
		}, {
			ItemType.Ruby, new ItemMeta() {
				Name = "Ruby",
				SpriteName = "roguelikeitems_31",
				Comment = "Fiery beauty",
			}
		},
	};
}
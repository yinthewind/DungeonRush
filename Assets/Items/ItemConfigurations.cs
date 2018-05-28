using System.Collections.Generic;

public enum ItemType {

	WoodenBow,
	IronSword,

	Sapphire,
	Ruby,

	ChainMail,

	SpeedAmulate,

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

public class ChainMail : Item {}

public class SpeedAmulate : Item {}

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
	public int DefenceBonus;

	public List<CardType> Cards;
}

public class ItemConfigurations {

	public Dictionary<ItemType, ItemMeta> ItemMetas = new Dictionary<ItemType, ItemMeta> () { { 
			ItemType.WoodenBow, new ItemMeta () {
				Name = "Wooden Bow",
				SpriteName = "roguelikeitems_87",
				Comment = "Common stuff",

				SpeedBonus = 1,
				Cards = new List<CardType>() {
					CardType.DoubleShot,
					CardType.DoubleShot,
					CardType.Shot,
					CardType.Shot,
					CardType.Shot,
					CardType.Miss,
				},
			}
		}, {
			ItemType.IronSword, new ItemMeta () {
				Name = "Iron Sword",
				SpriteName = "roguelikeitems_71",
				Comment = "Casual stuff",

				SpeedBonus = 0,
				Cards = new List<CardType>() {
					CardType.Stab,
					CardType.Stab,
					CardType.Slash,
					CardType.Slash,
					CardType.Slash,
					CardType.Miss,
				},
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
		}, {
			ItemType.ChainMail, new ItemMeta() {
				Name = "Chain Mail",
				SpriteName = "roguelikeitems_85",
				Comment = "Fine quality!",

				SpeedBonus = -1,
				DefenceBonus = 3,
			}
		}, {
			ItemType.SpeedAmulate, new ItemMeta() {
				Name = "Speed Amulate",
				SpriteName = "roguelikeitems_3",
				Comment = "Feel like wind!",

				SpeedBonus = 1,
			}
		},
	};
}

using System.Collections.Generic;

public enum ItemType {

	WoodenBow,
	IronSword,
	IronKnife,

	Sapphire,
	Ruby,

	ChainMail,

	SpeedAmulate,
}

public enum ItemCategory {
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

public class ItemMeta {
	public ItemCategory Category;

	public int Level;

	public string Name;
	public string Comment;
	public string SpriteName;

	public int SpeedBonus;
	public int AttachBonus;
	public int DefenceBonus;

	public List<CardType> Cards;
}

public class ItemConfigurations {

	public Dictionary<ItemType, ItemMeta> ItemMetas = new Dictionary<ItemType, ItemMeta> () { { 
			ItemType.WoodenBow, new ItemMeta () {
				Category = ItemCategory.Weapon,

				Name = "Wooden Bow",
				SpriteName = "roguelikeitems_87",
				Comment = "Common stuff",

				SpeedBonus = 1,
				AttachBonus = 4,
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
				Category = ItemCategory.Weapon,

				Name = "Iron Sword",
				SpriteName = "roguelikeitems_71",
				Comment = "Casual stuff",

				SpeedBonus = 0,
				AttachBonus = 7,
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
			ItemType.IronKnife, new ItemMeta () {
				Category = ItemCategory.Weapon,

				Name = "Iron Knife",
				SpriteName = "roguelikeitems_70",
				Comment = "Casual stuff",

				SpeedBonus = 0,
				AttachBonus = 3,
				Cards = new List<CardType>() {
					CardType.Stab,
					CardType.Slash,
					CardType.Miss,
					CardType.Miss,
				},
			}
		}, {
			ItemType.Sapphire, new ItemMeta() {
				Category = ItemCategory.Gem,

				Name = "Sapphire",
				SpriteName = "roguelikeitems_29",
				Comment = "Ice cold",
			}
		}, {
			ItemType.Ruby, new ItemMeta() {
				Category = ItemCategory.Gem,

				Name = "Ruby",
				SpriteName = "roguelikeitems_31",
				Comment = "Fiery beauty",

				AttachBonus = 1,
				Cards = new List<CardType>() {
					CardType.PerfectStrike,
					CardType.PerfectStrike,
					CardType.PerfectStrike,
				},
			}
		}, {
			ItemType.ChainMail, new ItemMeta() {
				Category = ItemCategory.Armor,

				Name = "Chain Mail",
				SpriteName = "roguelikeitems_85",
				Comment = "Fine quality!",

				SpeedBonus = -1,
				DefenceBonus = 3,
			}
		}, {
			ItemType.SpeedAmulate, new ItemMeta() {
				Category = ItemCategory.Amulate,

				Name = "Speed Amulate",
				SpriteName = "roguelikeitems_3",
				Comment = "Feel like wind!",

				SpeedBonus = 1,
			}
		},
	};
}

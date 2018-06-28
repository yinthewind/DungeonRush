using System.Collections.Generic;

public class ItemMeta {
	public ItemCategory Category;

	public int Level;

	public string Name;
	public string Comment;
	public string SpriteName;

	public int SpeedBonus;
	public int AttackBonus;
	public int DefenceBonus;

	public List<CardType> Cards;
}

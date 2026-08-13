using UnityEngine;

[System.Serializable] // This attribute allows the enum to be serialized and displayed in the Unity Inspector
public enum ItemCategory
{
    Material,
    Tool,
    Consumable,
    Catalyst
}

public class Item
{
    public string itemName;
    public int itemID;
    public ItemCategory category;
    public int stackSize;
    public int baseValue;

    public Item(string name, int id, ItemCategory category, int stackSize, int baseValue)
    {
        this.itemName = name;
        this.itemID = id;
        this.category = category;
        this.stackSize = stackSize;
        this.baseValue = baseValue;
    }
}
using UnityEngine;

public class ToolItem : Item
{
    public int durability;

    public ToolItem(string name, int id, int durability, int baseValue)
        : base(name, id, ItemCategory.Tool, 1, baseValue)
    {
        this.durability = durability;
    }

    public void UseTool()
    {
        durability--;
        UnityEngine.Debug.Log("Used " + itemName + ". Now has " + durability + " durability.");

        if (durability <= 0)
            BreakItem();
    }

    public void BreakItem()
    {
        UnityEngine.Debug.Log(itemName + " is broken.");
        // Later remove from inventory.
    }
}
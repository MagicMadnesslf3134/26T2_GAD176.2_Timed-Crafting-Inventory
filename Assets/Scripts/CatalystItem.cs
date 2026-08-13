using UnityEngine;

public class CatalystItem : Item
{
    public CatalystItem(string name, int id, int baseValue)
        : base(name, id, ItemCategory.Catalyst, 1, baseValue)
    {
    }

    public Item ModifyResult(Item baseResult)
    {
        // Increases base value by 20%
        int newValue = Mathf.RoundToInt(baseResult.baseValue * 1.2f);

        UnityEngine.Debug.Log("Used catalyst. New value is " + newValue);

        return new Item(
            baseResult.itemName + " (Enhanced)",
            baseResult.itemID,
            baseResult.category,
            baseResult.stackSize,
            newValue
        );
    }
}
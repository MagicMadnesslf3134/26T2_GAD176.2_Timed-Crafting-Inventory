using UnityEngine;

public class ConsumableItem : Item
{
    public float cooldownTime;

    public ConsumableItem(string name, int id, float cooldownTime, int baseValue)
        : base(name, id, ItemCategory.Consumable, 1, baseValue)
    {
        this.cooldownTime = cooldownTime;
    }

    public void Consume()
    {
        UnityEngine.Debug.Log("Consumed " + itemName + ".");
        // Later apply effects or cooldown.
    }
}
using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private Dictionary<Item, int> items = new Dictionary<Item, int>();

    public bool HasIngredients(Recipe recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            if (!items.ContainsKey(ingredient.Key) || items[ingredient.Key] < ingredient.Value)
                return false;
        }
        return true;
    }

    public void RemoveIngredients(Recipe recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            Item item = ingredient.Key;
            int amount = ingredient.Value;

            if (items.ContainsKey(item))
            {
                items[item] -= amount;
                if (items[item] <= 0)
                    items.Remove(item);
            }
        }
    }

    public void AddItem(Item item, int amount)
    {
        if (!items.ContainsKey(item))
            items[item] = 0;

        items[item] += amount;
    }

    public int GetItemCount(Item item)
    {
        if (!items.ContainsKey(item))
            return 0;

        return items[item];
    }

    public void RemoveItem(Item item, int amount)
    {
        if (!items.ContainsKey(item))
            return;

        items[item] -= amount;

        if (items[item] <= 0)
            items.Remove(item);
    }

    public Dictionary<Item, int> GetAllItems()
    {
        return new Dictionary<Item, int>(items);
    }
}
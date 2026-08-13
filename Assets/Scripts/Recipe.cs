using UnityEngine;
using System.Collections.Generic;

public class Recipe
{
    public Item outputItem;
    public Dictionary<Item, int> ingredients;

    public Recipe(Item outputItem, Dictionary<Item, int> ingredients)
    {
        this.outputItem = outputItem;
        this.ingredients = ingredients;
    }
}
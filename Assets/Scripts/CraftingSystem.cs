using UnityEngine;
using System.Collections.Generic;

public class CraftingSystem
{
    public Inventory inventory;

    public CraftingSystem(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void Craft(Recipe recipe)
    {
        if (!inventory.HasIngredients(recipe))
            return;

        inventory.RemoveIngredients(recipe);
        inventory.AddItem(recipe.outputItem, 1);

        UnityEngine.Debug.Log("Item crafted.");
    }

    public void CraftWithCatalyst(Recipe recipe, CatalystItem catalyst)
    {
        if (!inventory.HasIngredients(recipe))
        {
            UnityEngine.Debug.Log("Insufficient materials.");
            return;
        }
        inventory.RemoveIngredients(recipe);

        Item modifiedItem = catalyst.ModifyResult(recipe.outputItem);
        inventory.AddItem(modifiedItem, 1);

        UnityEngine.Debug.Log("Item crafted with catalyst.");
    }

    public List<Recipe> GetCraftableRecipes(List<Recipe> allRecipes)
    {
        List<Recipe> craftable = new List<Recipe>();

        foreach (Recipe recipe in allRecipes)
        {
            if (inventory.HasIngredients(recipe))
                craftable.Add(recipe);
        }

        return craftable;
    }
}
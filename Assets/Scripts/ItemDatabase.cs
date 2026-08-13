using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> allItems = new List<Item>();
    public List<Recipe> allRecipes = new List<Recipe>();

    void Awake()
    {
        // Items
        Item wood = new Item("Wood", 1, ItemCategory.Material, 99, 5);
        Item stone = new Item("Stone", 2, ItemCategory.Material, 99, 3);
        Item iron = new Item("Iron", 3, ItemCategory.Material, 99, 10);
        Item coal = new Item("Coal", 4, ItemCategory.Material, 99, 2);
        Item herb = new Item("Herb", 5, ItemCategory.Material, 99, 1);
        Item water = new Item("Water", 6, ItemCategory.Material, 99, 1);
        ToolItem pickaxe = new ToolItem("Pickaxe", 3, 10, 20);
        ToolItem sword = new ToolItem("Sword", 6, 15, 30);
        ConsumableItem potion = new ConsumableItem("Potion", 7, 5f, 20);

        allItems.Add(wood);
        allItems.Add(stone);
        allItems.Add(iron);
        allItems.Add(coal);
        allItems.Add(herb);
        allItems.Add(water);
        allItems.Add(pickaxe);
        allItems.Add(sword);
        allItems.Add(potion);

        // Recipe: Pickaxe
        Dictionary<Item, int> pickaxeIngredients = new Dictionary<Item, int>();
        pickaxeIngredients.Add(wood, 3);
        pickaxeIngredients.Add(stone, 2);

        Recipe pickaxeRecipe = new Recipe(pickaxe, pickaxeIngredients);
        allRecipes.Add(pickaxeRecipe);

        // Recipe: Sword
        Dictionary<Item, int> swordIngredients = new Dictionary<Item, int>();
        swordIngredients.Add(iron, 5);
        swordIngredients.Add(coal, 1);

        Recipe swordRecipe = new Recipe(sword, swordIngredients);
        allRecipes.Add(swordRecipe);

        // Recipe: Potion
        Dictionary<Item, int> potionIngredients = new Dictionary<Item, int>();
        potionIngredients.Add(herb, 2);
        potionIngredients.Add(water, 1);

        Recipe potionRecipe = new Recipe(potion, potionIngredients);
        allRecipes.Add(potionRecipe);
    }
}
using UnityEngine;

public class TestSetup : MonoBehaviour
{
    public CraftingUI craftingUI;

    private Inventory inventory;
    private CraftingSystem craftingSystem;

    void Start()
    {
        inventory = new Inventory();
        craftingSystem = new CraftingSystem(inventory);

        craftingUI.Initialize(craftingSystem);

        // Give player starting items
        inventory.AddItem(new Item("Wood", 1, ItemCategory.Material, 99, 5), 10);
        inventory.AddItem(new Item("Stone", 2, ItemCategory.Material, 99, 3), 10);
    }
}
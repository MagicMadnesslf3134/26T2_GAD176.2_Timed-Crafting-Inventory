using UnityEngine;

public class GatherMaterials : MonoBehaviour
{
    public PlayerInventoryController player;
    public ItemDatabase database;
    public InventoryUI inventoryUI;
    public CraftingUI craftingUI;

    public void Gather()
    {
        Debug.Log("Gathering materials...");
        Debug.Log("Player inventory before gathering: " + (player != null ? player.inventory : null));
        for (int i = 0; i < 3; i++)
        {
            Item randomItem = database.allItems[Random.Range(0, database.allItems.Count)];
            int amount = Random.Range(1, 4);

            player.inventory.AddItem(randomItem, amount);
        }
        if (inventoryUI != null)
            inventoryUI.Refresh();

        if (craftingUI != null)
            craftingUI.RefreshCraftButton();

            Debug.Log("Player inventory after gathering: ");
        foreach (var kvp in player.inventory.GetAllItems())
        {
            Debug.Log(kvp.Key.itemName + " x" + kvp.Value);
        }
    }
}
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private CraftingUI craftingUI;

    private Inventory inventory;
    private CraftingSystem craftingSystem;

    void Awake()
    {
        inventory = new Inventory();

        if (itemDatabase != null)
        {
            Item wood = itemDatabase.allItems.Find(i => i.itemName == "Wood");
            Item stone = itemDatabase.allItems.Find(i => i.itemName == "Stone");
            Item iron = itemDatabase.allItems.Find(i => i.itemName == "Iron");
            Item coal = itemDatabase.allItems.Find(i => i.itemName == "Coal");
            Item herb = itemDatabase.allItems.Find(i => i.itemName == "Herb");
            Item water = itemDatabase.allItems.Find(i => i.itemName == "Water");

            if (wood != null) inventory.AddItem(wood, 10);
            if (stone != null) inventory.AddItem(stone, 10);
            if (iron != null) inventory.AddItem(iron, 10);
            if (coal != null) inventory.AddItem(coal, 5);
            if (herb != null) inventory.AddItem(herb, 5);
            if (water != null) inventory.AddItem(water, 5);
        }

        craftingSystem = new CraftingSystem(inventory);

        if (craftingUI != null)
            craftingUI.Initialize(craftingSystem);
    }
}
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    public Inventory inventory;
    public CraftingSystem craftingSystem;

    void Awake()
    {
        Debug.Log("PlayerInventoryController Awake: Inventory created.");
        inventory = new Inventory();
        craftingSystem = new CraftingSystem(inventory);
    }
}
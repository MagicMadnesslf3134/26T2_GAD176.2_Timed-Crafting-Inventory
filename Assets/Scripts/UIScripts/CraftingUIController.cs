using UnityEngine;

public class CraftingUIController : MonoBehaviour
{
    public CraftingUI craftingUI;
    public PlayerInventoryController player;

    void Start()
    {
        craftingUI.Initialize(player.craftingSystem);
    }
}
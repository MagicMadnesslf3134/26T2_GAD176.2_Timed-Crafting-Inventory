using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventoryController player;
    public Transform listParent;
    public GameObject entryPrefab;

    public void Refresh()
    {
        if (player == null || player.inventory == null)
        {
            Debug.LogWarning("InventoryUI: Player or player inventory not ready yet.");
            return;
        }

        foreach (Transform child in listParent)
            Destroy(child.gameObject);

        foreach (var entry in player.inventory.GetAllItems())
        {
            GameObject obj = Instantiate(entryPrefab, listParent);
            obj.GetComponent<TextMeshProUGUI>().text = entry.Key.itemName + " x" + entry.Value;
        }
    }
}
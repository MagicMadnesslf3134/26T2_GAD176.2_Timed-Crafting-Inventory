using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class Inventory : MonoBehaviour
{
    [SerializeField]  
    private GameObject inventoryMenu;

    [SerializeField]
    private GameObject inventorySlot;

    [SerializeField]
    private GameObject inventoryItemSection;

    [SerializeField]
    private List<ItemSpace> itemSpace = new List<ItemSpace>();
    
    void Update()
    {
        //Checking if inventory isn't currently active and activating it
        if (!inventoryMenu.activeSelf && Keyboard.current.iKey.wasPressedThisFrame)
        {
            inventoryMenu.SetActive(true);
            //Turns off the Cursor lock in the movement Script so we can click around the menu
            Cursor.lockState = CursorLockMode.None;
        }
        //Checking if inventory is currently active and deactivating it
        else if (inventoryMenu.activeSelf && Keyboard.current.iKey.wasPressedThisFrame)
        {
            inventoryMenu.SetActive(false);
            //Turns on the Cursor lock so our mouse doesnt go off the screen
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    //AddItem checking if item already exists in inventory, creating if not and adding ItemSpace script to list, adding if does.
    //Variables sent to ItemSpace script to save the values there 
    public void AddItem(string itemName, Sprite sprite, string itemDescription, int itemAmount, double saleValue)
    {
        Debug.Log(itemName + " " + sprite + " " + itemDescription + " " + itemAmount + " " + saleValue);
        bool itemAdded = false;

        //Checking if we already have the specific Item in inventory
        for (int i = 0; i < itemSpace.Count; i++)
        {
            //Checking if item matches an item already in one of the scripts in the list
            if (itemSpace[i].containsItem && itemName == itemSpace[i].itemName)
            {
                itemSpace[i].AddItem(itemName, sprite, itemDescription, itemAmount, saleValue);
                itemAdded = true;
                break;
            }
        }
        //If no specific item already create new inventory slot with item assigned
        if (!itemAdded)
        {
            //Spawning clone as a child of inventoryItemSection
            GameObject slotClone = Instantiate(inventorySlot, inventoryItemSection.transform);
            itemSpace.Add(slotClone.GetComponent<ItemSpace>());
            itemSpace[itemSpace.Count - 1].AddItem(itemName, sprite, itemDescription, itemAmount, saleValue);
        }
    }
    
    //Checking Which script matches the input itemName
    public int GetItemSpaceNumber(string itemName)
    {
        for (int i = 0; i < itemSpace.Count; i++)
        {
            if(itemSpace[i].itemName == itemName)
            {
                return i;
            }
        }
        return -1;
    }

    //Remove ItemSpace from the list
    public void RemoveItemSpace(int GetItemSpaceNumber)
    {
        itemSpace.Remove(itemSpace[GetItemSpaceNumber]);
    }   
}

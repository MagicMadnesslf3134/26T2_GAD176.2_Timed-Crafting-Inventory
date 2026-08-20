using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected string itemName;

    [SerializeField] 
    protected Sprite sprite;
    [SerializeField]
    protected string itemDescription;
    [SerializeField]
    protected int itemAmount;
    [SerializeField]
    protected double saleValue;

    public Inventory inventory;

    private bool canPickUp;

    private void Start()
    {
        //Finding the inventory script
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    //Set the item to be pickupable inside of range
    private void OnTriggerEnter(Collider other)
    {
        canPickUp = true;
    }
    //Checks if Player is within range of the object and pressed e to trigger PickUp()
    //UseItem here to demo inheritence
    private void Update()
    {
        if (canPickUp && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            UseItem();
        }
    }

    //Set the item to no longer be pickupable outside of range
    private void OnTriggerExit(Collider other)
    {
        canPickUp = false;
    }
    //Default value for UseItem (Changed in inheritance)
    protected virtual void UseItem()
    {
        Debug.Log("Item Used");
    }
    //Reset canPickUp to false to prevent infinite item creation
    //Send variables to AddItem in the inventory script 
    //Remove object from scene
    void PickUp()
    {
        canPickUp = false;
        inventory.AddItem(itemName, sprite, itemDescription, itemAmount, saleValue);
        //Destroy(gameObject);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
public class ItemSpace : MonoBehaviour
{
    private Events events;

    public string itemName;
    public Sprite itemSprite;
    string itemDescription;
    public double saleValue;
    public int itemCount;
    public bool containsItem;

    [SerializeField]
    private TextMeshProUGUI itemCounter;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image itemInspectorImage;

    [SerializeField]
    private TextMeshProUGUI itemInspectorDescription;

    [SerializeField]
    private Inventory inventoryScript;

    [SerializeField]
    private Button removeButton;
    
    void Start()
    {
        //Getting references for scripts that the prefab requires reference for by checking objects in the scene
        GameObject itemInspectorImageObject = GameObject.Find("ItemInspectorImage");
        itemInspectorImage = itemInspectorImageObject.GetComponent<Image>();

        GameObject itemInspectorDescriptionObject = GameObject.Find("ItemInspectorDescription");
        itemInspectorDescription = itemInspectorDescriptionObject.GetComponent<TextMeshProUGUI>();

        GameObject inventoryGameObject = GameObject.Find("Inventory");
        inventoryScript = inventoryGameObject.GetComponent<Inventory>();

        GameObject removeButtonGameObject = GameObject.Find("Remove Item");
        removeButton = removeButtonGameObject.GetComponent<Button>();

        events = GameObject.FindGameObjectWithTag("Events").GetComponent<Events>();
    }

    
    
    //Values recieved from AddItem Inventory sent to this script through here
    public void AddItem(string itemName, Sprite sprite, string itemDescription, int itemAmount, double saleValue)
    {
        //this used to differenciate variables being recieved from variables this script has
        this.itemName = itemName;
        itemSprite = sprite;
        this.itemDescription = itemDescription;
        this.itemCount += itemAmount;
        this.saleValue = saleValue;
 
        containsItem = true;

        //Changing visuals on the screen for items
        //(String)itemCount doesn't work but itemCount.ToString does
        itemCounter.text = itemCount.ToString();
        itemImage.sprite = itemSprite;

        //Checking if clock is the item added and sending signal out
        if(this.itemName == "Clock")
        {
            events.onPickUpClock.Invoke();
        }
        
    }
    //Reduces itemCount by 1 to be used in other functions e.g. use, sell, discard
    //If item count hits 0 or less removes reference from inventory script list and destroy self
    public void RemoveItem()
    {
            itemCount -= 1;

        //Changing visuals on the screen for items
        //(String)itemCount doesn't work but itemCount.ToString does
        itemCounter.text = itemCount.ToString();
        itemImage.sprite = itemSprite;

        //Changing visuals on the screen for items
        //(String)itemCount doesn't work but itemCount.ToString does
        itemCounter.text = itemCount.ToString();
        itemImage.sprite = itemSprite;

        //Checking if final item drop was Clock then invoking drop clock to remove clock from player UI
        if (itemCount <= 0 && itemName == "Clock")
        {
            events.onDropClock.Invoke();
        }
        if (itemCount <= 0)
        {
            inventoryScript.RemoveItemSpace(inventoryScript.GetItemSpaceNumber(itemName));
            Destroy(gameObject);
        }
    }
    //Only has destroy functionality as Effect would be gained from inheritance in an item script
    public void UseItem()
    {
        //Using item specific Use functions
        events.onUseItem.Invoke();

        //Effect Happens here then

        RemoveItem();
    }
    //Only has destroy functionality as Effect would be gained from inheritance in an item script
    public void SellItem()
    {
        //Effect Happens here then

        RemoveItem();
    }
    //Sets inspector visuals to display item image and description text
    //Provides remove button ability to remove items from this script and removes other listeners prior to prevent other scripts being effected
    public void DisplayInInspector()
    {
        itemInspectorImage.sprite = itemSprite;
        itemInspectorDescription.text = itemDescription.ToString();
        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(RemoveItem);

    }

 
}

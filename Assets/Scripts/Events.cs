using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    //Used to send info from inventory script to Day/Night Script
    public UnityEvent onPickUpClock;
    //Used to send info from inventory script to Day/Night Script
    public UnityEvent onDropClock;
    //Used to inform items that we wish to use their functions 
    public UnityEvent onUseItem;

    //Used to send info from crafting script to inventory Script
    public UnityEvent onItemCrafted;

    //Used to send info from day/night to crafting script
    public UnityEvent onIsDay;

    //Used to send info from day/night to crafting script
    public UnityEvent onIsNight;



}

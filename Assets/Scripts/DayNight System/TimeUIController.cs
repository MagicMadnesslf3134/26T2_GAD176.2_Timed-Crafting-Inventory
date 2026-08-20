using TMPro;
using UnityEngine;

// Displays the current in-game time when the inventory system
// reports that the player owns a clock.
public class TimeUIController : TimeReactiveSystem
{
    [Header("UI")]
    [Tooltip(
        "The parent object containing the visible clock UI. " +
        "Do not assign the object containing this script.")]
    [SerializeField]
    private GameObject timeUIRoot;

    [SerializeField]
    private TMP_Text timeText;

    // This value is supplied by the inventory system.
    // TimeUIController does not decide whether the player has a clock.
    private bool hasClock;

    protected override void OnSystemInitialized()
    {
        UpdateClockVisibility();
    }

    protected override void UpdateSystem()
    {
        if (!hasClock || timeText == null)
        {
            return;
        }

        int hour = timeProvider.CurrentHour;
        int minute = timeProvider.CurrentMinute;

        // Converts 24-hour time into 12-hour clock format.
        string suffix = hour >= 12 ? "PM" : "AM";

        int displayHour = hour % 12;

        if (displayHour == 0)
        {
            displayHour = 12;
        }

        timeText.text =
            $"{displayHour}:{minute:00} {suffix}";
    }

    // The inventory system calls or invokes this method whenever
    // its existing hasClock value changes.
    public void OnHasClockChanged(bool newHasClockValue)
    {
        hasClock = newHasClockValue;
        UpdateClockVisibility();
    }

    private void UpdateClockVisibility()
    {
        if (timeUIRoot != null)
        {
            timeUIRoot.SetActive(hasClock);
            return;
        }

        // Fallback if no clock UI parent has been assigned.
        if (timeText != null)
        {
            timeText.gameObject.SetActive(hasClock);
        }
    }
}
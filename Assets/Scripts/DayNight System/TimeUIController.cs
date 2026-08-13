using TMPro;
using UnityEngine;

// Displays the current in-game time when the inventory reports that the player owns a clock.
public class TimeUIController : TimeReactiveSystem
{
    [Header("UI")]
    [Tooltip("The clock UI container. Do not assign the GameObject containing this script.")]
    [SerializeField] private GameObject timeUIRoot;

    [SerializeField] private TMP_Text timeText;

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

        string suffix = hour >= 12 ? "PM" : "AM";

        int displayHour = hour % 12;

        if (displayHour == 0)
        {
            displayHour = 12;
        }

        timeText.text = $"{displayHour}:{minute:00} {suffix}";
    }

    // This receives the hasClock value announced by the inventory system.
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
        }
        else if (timeText != null)
        {
            timeText.gameObject.SetActive(hasClock);
        }
    }
}
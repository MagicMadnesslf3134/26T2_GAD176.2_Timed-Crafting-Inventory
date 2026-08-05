using TMPro;
using UnityEngine;

// Displays the current in-game time on the screen.
public class TimeUIController : TimeReactiveSystem
{
    [Header("UI")]
    [SerializeField] private TMP_Text timeText;

    protected override void UpdateSystem()
    {
        int hour = timeProvider.CurrentHour;
        int minute = timeProvider.CurrentMinute;

        // Converts 24-hour time into a readable 12-hour clock format.
        string suffix = hour >= 12 ? "PM" : "AM";

        int displayHour = hour % 12;
        if (displayHour == 0)
        {
            displayHour = 12;
        }

        // Updates the UI text so the player can see the current time of day.
        timeText.text = $"{displayHour}:{minute:00} {suffix}";
    }
}
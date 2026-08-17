using System;

// Interface used to provide time data to other systems without forcing them to directly depend on DayNightManager.
public interface ITimeProvider
{
    // Returns the current day progress as a value between 0 and 1.
    float CurrentTime01 { get; }

    // Returns the current in-game hour in 24-hour format.
    int CurrentHour { get; }

    // Returns the current in-game minute.
    int CurrentMinute { get; }

    // Returns true only on the frame when a new day begins.
    bool IsNewDay { get; }

    // Returns true from sunrise until sunset.
    bool IsDay { get; }

    // Returns true from sunset until sunrise.
    bool IsNight { get; }

    // Announced when the time crosses the configured sunrise time.
    event Action SunriseOccurred;

    // Announced when the time crosses the configured sunset time.
    event Action SunsetOccurred;
}
// Interface used to provide time data to other systems without forcing them to directly depend on DayNightManager.
public interface ITimeProvider
{
    //returns the current day progress as a value between 0 and 1
    float CurrentTime01 { get; }
    //returns the current in game time in 24 hour format
    int CurrentHour { get; }
    //returns the current in game minute
    int CurrentMinute { get; }
    //returns true only when a new day begins
    bool IsNewDay { get; }
}

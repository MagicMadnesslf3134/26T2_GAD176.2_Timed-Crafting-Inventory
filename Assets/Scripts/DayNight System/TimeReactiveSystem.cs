using UnityEngine;

public abstract class TimeReactiveSystem : MonoBehaviour
{
    //base classfor any part of the system that reacts to the current time of day
    protected ITimeProvider timeProvider;

    // Receives the time provider dependency from the DayNightManager.
    public void Initialize(ITimeProvider provider)
    {
        timeProvider = provider;
        OnSystemInitialized();
    }

    protected virtual void OnSystemInitialized() { }

    protected abstract void UpdateSystem();
    
    // Forces child classes to define their own time-based behaviour.
    private void Update()
    {
        if (timeProvider == null) return;

        UpdateSystem();
    }
}
using UnityEngine;

// Base class for any system that reacts to the current time of day.
public abstract class TimeReactiveSystem : MonoBehaviour
{
    protected ITimeProvider timeProvider;

    // Receives the time provider dependency from the DayNightManager.
    public void Initialize(ITimeProvider provider)
    {
        if (provider == null)
        {
            Debug.LogWarning(
                $"{name} could not initialize because the time provider was null.",
                this);

            return;
        }

        timeProvider = provider;
        OnSystemInitialized();
    }

    // Child classes can override this to perform setup or subscribe to events.
    protected virtual void OnSystemInitialized() { }

    // Forces child classes to define their own time-based behaviour.
    protected abstract void UpdateSystem();

    private void Update()
    {
        if (timeProvider == null)
        {
            return;
        }

        UpdateSystem();
    }
}
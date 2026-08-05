
using UnityEngine;

public class DayNightManager : MonoBehaviour, ITimeProvider
{
    [Header("Cycle Settings")]
    // Controls how long one full 24-hour cycle lasts in real-time minutes.
    [SerializeField] private float cycleLengthMinutes = 30f;

    [Header("Connected Systems")]
    // Stores all systems that need to react to the current time of day.
    [SerializeField] private TimeReactiveSystem[] timeSystems;

    // Encapsulated time data that other scripts can read but not directly modify.
    private float currentCycleTime;
    private float cycleLengthSeconds;
    private bool isNewDay;

    public float CurrentTime01 => currentCycleTime / cycleLengthSeconds;
    public int CurrentHour => Mathf.FloorToInt(CurrentTime01 * 24f);
    public int CurrentMinute => Mathf.FloorToInt((CurrentTime01 * 24f - CurrentHour) * 60f);
    public bool IsNewDay => isNewDay;

    private void Start()
    {
        cycleLengthSeconds = cycleLengthMinutes * 60f;
        currentCycleTime = 0f;
        
        // Injects this manager as the time provider for each connected system.
        foreach (TimeReactiveSystem system in timeSystems)
        {
            system.Initialize(this);
        }
    }

    private void Update()
    {
        isNewDay = false;

        currentCycleTime += Time.deltaTime;

        // When the cycle reaches the end of the day, reset back to midnight.
        if (currentCycleTime >= cycleLengthSeconds)
        {
            currentCycleTime = 0f;
            isNewDay = true;
        }
    }
}
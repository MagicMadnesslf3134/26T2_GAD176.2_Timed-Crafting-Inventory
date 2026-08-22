using System;
using UnityEngine;
using UnityEngine.Events;

public class DayNightManager : MonoBehaviour, ITimeProvider
{
    [Header("Cycle Settings")]
    [Tooltip("How long one complete 24-hour cycle lasts in real-time minutes.")]
    [SerializeField, Min(0.01f)]
    private float cycleLengthMinutes = 30f;

    [Header("Sunrise and Sunset")]
    [Tooltip("The in-game hour at which daytime begins.")]
    [SerializeField, Range(0f, 24f)]
    private float sunriseHour = 6f;

    [Tooltip("The in-game hour at which nighttime begins.")]
    [SerializeField, Range(0f, 24f)]
    private float sunsetHour = 18f;

    [Header("Connected Systems")]
    [SerializeField]
    private TimeReactiveSystem[] timeSystems;

    [Header("Inspector Events")]
    [Tooltip("Invoked when sunrise occurs.")]
    [SerializeField]
    private UnityEvent onSunrise;

    [Tooltip("Invoked when sunset occurs.")]
    [SerializeField]
    private UnityEvent onSunset;

    private float currentCycleTime;
    private float cycleLengthSeconds;
    private bool isNewDay;
    private bool isDay;
    private bool previousIsDay;

    public float CurrentTime01
    {
        get
        {
            if (cycleLengthSeconds <= 0f)
            {
                return 0f;
            }

            return currentCycleTime / cycleLengthSeconds;
        }
    }

    public int CurrentHour
    {
        get
        {
            return Mathf.FloorToInt(CurrentTime01 * 24f) % 24;
        }
    }

    public int CurrentMinute
    {
        get
        {
            float currentTimeInHours = CurrentTime01 * 24f;
            float hourFraction =
                currentTimeInHours - Mathf.Floor(currentTimeInHours);

            return Mathf.FloorToInt(hourFraction * 60f);
        }
    }

    public bool IsNewDay => isNewDay;
    public bool IsDay => isDay;
    public bool IsNight => !isDay;

    public event Action SunriseOccurred;
    public event Action SunsetOccurred;

    private void Awake()
    {
        cycleLengthSeconds =
            Mathf.Max(0.01f, cycleLengthMinutes * 60f);

        // The cycle begins at midnight.
        currentCycleTime = 0f;

        isDay = CalculateIsDay(CurrentTime01);
        previousIsDay = isDay;
    }

    private void Start()
    {
        InitializeConnectedSystems();
    }

    private void Update()
    {
        isNewDay = false;
        currentCycleTime += Time.deltaTime;

        if (currentCycleTime >= cycleLengthSeconds)
        {
            currentCycleTime %= cycleLengthSeconds;
            isNewDay = true;
        }

        UpdateDayNightState();
    }

    private void InitializeConnectedSystems()
    {
        if (timeSystems == null)
        {
            return;
        }

        foreach (TimeReactiveSystem system in timeSystems)
        {
            if (system != null)
            {
                system.Initialize(this);
            }
        }
    }

    private void UpdateDayNightState()
    {
        isDay = CalculateIsDay(CurrentTime01);

        if (isDay == previousIsDay)
        {
            return;
        }

        if (isDay)
        {
            AnnounceSunrise();
        }
        else
        {
            AnnounceSunset();
        }

        previousIsDay = isDay;
    }

    private bool CalculateIsDay(float time01)
    {
        float currentHour = time01 * 24f;

        if (sunriseHour < sunsetHour)
        {
            return currentHour >= sunriseHour &&
                   currentHour < sunsetHour;
        }

        return currentHour >= sunriseHour ||
               currentHour < sunsetHour;
    }

    private void AnnounceSunrise()
    {
        SunriseOccurred?.Invoke();
        onSunrise?.Invoke();

        Debug.Log("Sunrise occurred.", this);
    }

    private void AnnounceSunset()
    {
        SunsetOccurred?.Invoke();
        onSunset?.Invoke();

        Debug.Log("Sunset occurred.", this);
    }
}
using UnityEngine;

// Controls sun, moon, and ambient lighting based on the current time of day.
public class SkyLightingController : TimeReactiveSystem
{
    [Header("References")]
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;

    [Header("Sun")]
    [SerializeField] private AnimationCurve sunIntensityCurve;
    [SerializeField] private Gradient sunColourGradient;

    [Header("Moon")]
    [SerializeField] private AnimationCurve moonIntensityCurve;

    [Header("Environment")]
    [SerializeField] private AnimationCurve ambientIntensityCurve;
    [SerializeField] private Gradient ambientColourGradient;

    [Header("Rotation")]
    [SerializeField] private float sunriseOffset = -90f;

    protected override void UpdateSystem()
    {
        if (timeProvider == null) return;

        float time = timeProvider.CurrentTime01;

        RotateLights(time);
        UpdateLighting(time);
    }

    // Rotates the sun and moon so their positions visually match the time of day.
    private void RotateLights(float time)
    {
        float rotation = (time * 360f) + sunriseOffset;

        if (sun != null)
        {
            sun.transform.rotation = Quaternion.Euler(rotation, 170f, 0f);
        }

        if (moon != null)
        {
            moon.transform.rotation = Quaternion.Euler(rotation + 180f, 170f, 0f);
        }
    }

    // Adjusts the brightness of the environment as the day progresses according to the values set in the intensity curves
    private void UpdateLighting(float time)
    {
        if (sun != null)
        {
            sun.intensity = sunIntensityCurve.Evaluate(time);
            sun.color = sunColourGradient.Evaluate(time);
        }

        if (moon != null)
        {
            moon.intensity = moonIntensityCurve.Evaluate(time);
        }

        RenderSettings.ambientIntensity = ambientIntensityCurve.Evaluate(time);
        RenderSettings.ambientLight = ambientColourGradient.Evaluate(time);
    }
}
using UnityEngine;

// Controls the sun, moon, and ambient lighting
// based on the current time of day.
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
        float time = timeProvider.CurrentTime01;

        RotateLights(time);
        UpdateLighting(time);
    }

    // Rotates the sun and moon so their positions
    // visually match the time of day.
    private void RotateLights(float time)
    {
        float rotation = (time * 360f) + sunriseOffset;

        if (sun != null)
        {
            sun.transform.rotation =
                Quaternion.Euler(rotation, 170f, 0f);
        }

        if (moon != null)
        {
            moon.transform.rotation =
                Quaternion.Euler(rotation + 180f, 170f, 0f);
        }
    }

    // Adjusts the brightness and colour of the environment.
    private void UpdateLighting(float time)
    {
        if (sun != null)
        {
            if (sunIntensityCurve != null)
            {
                sun.intensity = sunIntensityCurve.Evaluate(time);
            }

            if (sunColourGradient != null)
            {
                sun.color = sunColourGradient.Evaluate(time);
            }
        }

        if (moon != null && moonIntensityCurve != null)
        {
            moon.intensity = moonIntensityCurve.Evaluate(time);
        }

        if (ambientIntensityCurve != null)
        {
            RenderSettings.ambientIntensity =
                ambientIntensityCurve.Evaluate(time);
        }

        if (ambientColourGradient != null)
        {
            RenderSettings.ambientLight =
                ambientColourGradient.Evaluate(time);
        }
    }
}
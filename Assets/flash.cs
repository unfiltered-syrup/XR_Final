using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
    public Light lightToBlink; // Assign the Light component in the inspector
    public float blinkInterval = 1f; // Time in seconds between blinks
    public float minIntensity = 0f; // Minimum intensity of the light
    public float maxIntensity = 1f; // Maximum intensity of the light
    private bool isBlinking = false; // To control the blinking state

    // Start is called before the first frame update
    void Start()
    {
        if (lightToBlink == null)
        {
            // If no light is assigned, try to find a Light component on the same GameObject
            lightToBlink = GetComponent<Light>();
        }
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        // This loop runs forever
        while (true)
        {
            // Check if we are currently blinking
            if (isBlinking)
            {
                // If so, set the light's intensity to minimum
                lightToBlink.intensity = minIntensity;
            }
            else
            {
                // If not, set the light's intensity to maximum
                lightToBlink.intensity = maxIntensity;
            }
            // Invert the blinking state
            isBlinking = !isBlinking;

            // Wait for the specified interval before continuing the loop
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
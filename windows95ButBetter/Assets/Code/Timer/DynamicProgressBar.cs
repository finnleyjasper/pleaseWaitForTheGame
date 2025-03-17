using UnityEngine;

public class DynamicProgressBar : MonoBehaviour
{
    // Reference to the sprite renderer
    private SpriteRenderer spriteRenderer;

    // Countdown time (in seconds)
    public float countdownTime = 60f;

    // The maximum scale multiplier (final scale: 31.47341x the original width)
    public float maxScaleMultiplier = 31.47341f;

    // The target final X position
    public float finalXPosition = -0.00107f;

    // The initial scale of the sprite (at the start of the countdown)
    private Vector3 initialScale;

    void Start()
    {
        // Get the SpriteRenderer component of the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Save the initial scale of the sprite
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Only scale if countdown is running
        if (countdownTime > 0)
        {
            // Decrease countdown timer
            countdownTime -= Time.deltaTime;

            // Calculate the scale factor based on the remaining time
            // This will linearly increase the scale from 1x to 31.47341x as the countdown goes from 60 to 0
            float scaleFactor = Mathf.Lerp(1f, maxScaleMultiplier, (60f - countdownTime) / 60f);

            // Apply the scale to the sprite's X-axis (we only scale the X axis)
            transform.localScale = new Vector3(initialScale.x * scaleFactor, initialScale.y, initialScale.z);

            // Adjust the X position to match the final desired value (keeping the left edge in place)
            float adjustedXPosition = Mathf.Lerp(0f, finalXPosition, (60f - countdownTime) / 60f);
            transform.localPosition = new Vector3(adjustedXPosition, transform.localPosition.y, transform.localPosition.z);
        }
    }
}

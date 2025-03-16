using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    public float targetAspect = 4f / 3f; // 4:3 aspect ratio

    void Start()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f) // If the screen is wider than the target aspect ratio
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else // If the screen is taller than the target aspect ratio
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
}

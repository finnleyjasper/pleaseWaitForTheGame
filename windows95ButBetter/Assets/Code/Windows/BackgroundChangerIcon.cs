using UnityEngine;

public class BackgroundChangerIcon : DesktopIcon
{
    public SpriteRenderer backgroundRenderer; // Assign HomeScreenGradient_0 in Inspector
    public Sprite[] backgrounds; // Assign HomeScreenGradient, wp_hills, wp_moon, wp_sunset in Inspector
    private int currentBackgroundIndex = 0;

    override public void OnMouseDown()
    {
        // Handle double-click detection
        if (Time.time - lastClickTime < doubleClickTime)
        {
            // Double-click detected, change the background
            ChangeBackground();
            LoadManager manager = GameObject.Find("Manager").GetComponent<LoadManager>();
            manager.AddInteraction(1);
            GameObject.Find("Manager").GetComponent<AudioManager>().PlayClip("click");
        }
        else
        {
            // First click, spawn selection overlay
            if (activeOverlay == null && SelectionOverlayPrefab != null)
            {
                activeOverlay = Instantiate(SelectionOverlayPrefab, transform.position, Quaternion.identity, transform);
                Debug.Log("Overlay created on icon click");
                DesktopManager.Instance.RegisterHighlightObject(activeOverlay);
            }
        }

        lastClickTime = Time.time;
    }

    void ChangeBackground()
    {
        if (backgroundRenderer != null && backgrounds.Length > 0)
        {
            // Cycle through backgrounds
            currentBackgroundIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
            backgroundRenderer.sprite = backgrounds[currentBackgroundIndex];

            Debug.Log("Background changed to: " + backgroundRenderer.sprite.name);
        }
        else
        {
            Debug.LogWarning("Background renderer or backgrounds array is not set!");
        }
    }
}

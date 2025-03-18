using UnityEngine;

public class DesktopIcon : MonoBehaviour
{
    public GameObject SelectionOverlayPrefab; 
    public GameObject WindowPrefab; 
    public Transform WindowSpawnLocation;
    private GameObject activeOverlay;
    private float lastClickTime = 0f;
    private float doubleClickTime = 0.3f; // Max time between clicks 

    void OnMouseDown()
    {
        // Handle double-click detection
        if (Time.time - lastClickTime < doubleClickTime)
        {
            // Double-click detected, open the window
            OpenWindow();
        }
        else
        {
            // First click, spawn overlay
            if (activeOverlay == null && SelectionOverlayPrefab != null)
            {
                // Create the overlay and make it a child of the icon to track it easily
                activeOverlay = Instantiate(SelectionOverlayPrefab, transform.position, Quaternion.identity, transform);
                Debug.Log("Overlay created on icon click");
                DesktopManager.Instance.RegisterHighlightObject(activeOverlay);
            }
        }

        lastClickTime = Time.time;
    }

    void OpenWindow()
    {
        if (WindowPrefab != null)
        {
            Instantiate(WindowPrefab, WindowSpawnLocation.position, Quaternion.identity);
            Debug.Log("Window opened for this icon");
        }
    }

    public void Deselect()
    {
        // When deselected, destroy the overlay (if it exists)
        if (activeOverlay != null)
        {
            Debug.Log("Destroying overlay");
            Destroy(activeOverlay);  // Properly destroy the overlay
            activeOverlay = null;  // Reset the reference
        }
    }
}

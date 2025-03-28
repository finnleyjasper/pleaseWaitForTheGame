using UnityEngine;
using UnityEngine.UIElements;

public class DesktopIcon : MonoBehaviour
{
    public GameObject SelectionOverlayPrefab;
    public GameObject WindowPrefab;
    public Transform WindowSpawnLocation;
    public GameObject activeOverlay;
    public float lastClickTime = 0f;
    public float doubleClickTime = 0.3f; // Max time between clicks


    virtual public void OnMouseDown()
    {
        // Handle double-click detection
        if (Time.time - lastClickTime < doubleClickTime)
        {
            // Double-click detected, open the window
            OpenWindow();
            GameObject.Find("Manager").GetComponent<AudioManager>().PlayClip("click");

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
            DraggableWindow draggableWindow = WindowPrefab.GetComponent<DraggableWindow>();

            if (draggableWindow != null)
            {
                draggableWindow.enabled = true;
            }
            else
            {
                Debug.LogWarning("DraggableWindow script not found on the window prefab.");
            }
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

    void OnMouseEnter()
    {
        GameObject.Find("Manager").GetComponent<LoadManager>().CursorClick();
    }

    void OnMouseExit()
    {
        GameObject.Find("Manager").GetComponent<LoadManager>().CursorReset();
    }
}

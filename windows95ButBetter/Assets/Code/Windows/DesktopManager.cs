using UnityEngine;
using System.Collections.Generic;

public class DesktopManager : MonoBehaviour
{
    public static DesktopManager Instance;
    private List<DesktopIcon> icons = new List<DesktopIcon>();
    private List<GameObject> highlightObjects = new List<GameObject>();  // Store all instances of Highlight_0 prefab

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void RegisterIcon(DesktopIcon icon)
    {
        if (!icons.Contains(icon))
        {
            icons.Add(icon);
        }
    }

    public void RegisterHighlightObject(GameObject highlight)
    {
        if (!highlightObjects.Contains(highlight))
        {
            highlightObjects.Add(highlight);
        }
    }

    public void DeselectAllIcons()
    {
        Debug.Log("Deselecting all icons...");

        // Destroy all instances of Highlight_0
        foreach (var highlight in highlightObjects)
        {
            if (highlight != null)
            {
                Debug.Log("Destroying Highlight_0 overlay");
                Destroy(highlight);
            }
        }
        highlightObjects.Clear();  // Clear the list after destroying
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect click
        {
            // If the click is on an icon, nothing happens.
            if (IsClickOnIcon())
                return;

            // Otherwise, deselect all icons (clicking anywhere else)
            DeselectAllIcons();
        }
    }

    // Checks if the click is on any icon
    private bool IsClickOnIcon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            DesktopIcon clickedIcon = hit.collider.GetComponent<DesktopIcon>();
            if (clickedIcon != null)
            {
                Debug.Log("Clicked on an icon");
                return true; // Clicked on an icon
            }
        }

        return false; // Clicked outside icons
    }
}

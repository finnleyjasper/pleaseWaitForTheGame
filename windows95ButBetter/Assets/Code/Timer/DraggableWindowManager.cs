using UnityEngine;

public class DraggableWindowManager : MonoBehaviour
{
    void Update()
    {
        // Find all DraggableWindow components in the scene
        DraggableWindow[] draggableWindows = FindObjectsOfType<DraggableWindow>();

        // Loop through all found DraggableWindow components
        foreach (DraggableWindow draggableWindow in draggableWindows)
        {
            // Ensure that the DraggableWindow script is enabled
            if (!draggableWindow.enabled)
            {
                draggableWindow.enabled = true;
            }

            // Optionally, check if the camera has been assigned
            if (draggableWindow.canvas.worldCamera == null)
            {
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    draggableWindow.canvas.worldCamera = mainCamera;
                }
                else
                {
                    Debug.LogError("Main Camera not found in the scene!");
                }
            }
        }
    }
}

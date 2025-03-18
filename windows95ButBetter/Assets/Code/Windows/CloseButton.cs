using UnityEngine;

public class CloseButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Find the parent DraggableWindow and destroy it
        DraggableWindow draggableWindow = GetComponentInParent<DraggableWindow>();

        if (draggableWindow != null)
        {
            // Destroy the parent window (the DraggableWindow)
            Destroy(draggableWindow.gameObject);

            Debug.Log("Window closed");
        }
        else
        {
            Debug.LogError("No DraggableWindow found as parent.");
        }
    }
}

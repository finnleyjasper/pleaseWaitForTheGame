using UnityEngine;

public class DraggableWindow : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    void Update()
    {
        // If the mouse is clicked, check if it's over the object
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOver())
            {
                isDragging = true;
                // Calculate the offset between the object position and mouse position when dragging starts
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                // If the mouse is not over the object, do not start dragging
            }
        }

        // If dragging, move the object with the mouse position
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Update the object position based on mouse movement while maintaining the offset
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }

        // Stop dragging when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    // Check if the mouse is currently over the object
    bool IsMouseOver()
    {
        // Convert the mouse position to world space (make sure it's in 2D)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Set the z-value to 0 for 2D space

        Collider2D collider = GetComponent<Collider2D>();

        // Check if the mouse is within the object's bounds
        return collider.bounds.Contains(mousePos);
    }
}

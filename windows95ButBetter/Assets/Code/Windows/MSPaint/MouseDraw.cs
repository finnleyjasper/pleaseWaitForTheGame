using UnityEngine;

public class MouseDraw : MonoBehaviour
{
    private bool isDrawing = false;  // Track if the mouse is being held down
    private Vector2 lastMousePosition;  // To store the last mouse position

    [SerializeField] private Camera canvasWorldCamera;  // The camera rendering the canvas (use the same camera as the draggable window)
    [SerializeField] private GameObject drawingCanvas;  // The canvas where the drawing will be applied
    [SerializeField] private float lineWidth = 0.1f;  // Width of the drawn lines
    [SerializeField] private RectTransform windowRectTransform;  // RectTransform of the window to check bounds

    private DraggableWindow draggableWindow;  // Reference to the DraggableWindow script to disable dragging temporarily

    private void Awake()
    {
        // Dynamically assign the scene's main camera if the canvasWorldCamera is not set in the inspector
        if (canvasWorldCamera == null)
        {
            canvasWorldCamera = Camera.main;
        }

        if (canvasWorldCamera == null)
        {
            Debug.LogError("No camera found in the scene!");
        }

        if (drawingCanvas == null)
        {
            Debug.LogError("Drawing canvas not assigned!");
        }

        // Get the DraggableWindow component from the parent window
        draggableWindow = GetComponentInParent<DraggableWindow>();
        if (draggableWindow == null)
        {
            Debug.LogError("No DraggableWindow component found in the parent!");
        }
    }

    private void Update()
    {
        // Detect if the mouse is pressed and held
        if (Input.GetMouseButton(0))  // 0 = left mouse button
        {
            if (!isDrawing)
            {
                isDrawing = true;  // Start drawing
                lastMousePosition = GetMouseWorldPosition();  // Set the initial position to where the mouse is
                // Lock the draggable window from moving
                LockWindowDragging(true);
            }

            // If drawing, call the function to draw
            DrawLine();
        }
        else
        {
            if (isDrawing)
            {
                // Unlock the draggable window when the mouse is released
                LockWindowDragging(false);
            }
            isDrawing = false;  // Stop drawing when the mouse button is released
        }
    }

    private void DrawLine()
    {
        Vector2 currentMousePosition = GetMouseWorldPosition();

        // Only draw if the mouse has moved and it's inside the window's bounds
        if (currentMousePosition != lastMousePosition && IsMouseInsideWindow(currentMousePosition))
        {
            // Create a new line segment from the last mouse position to the current one
            DrawLineSegment(lastMousePosition, currentMousePosition);
            lastMousePosition = currentMousePosition;  // Update the last mouse position
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        // Convert the mouse position from screen space to world space using the canvas' camera
        Vector3 mouseWorldPosition = canvasWorldCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
    }

    private bool IsMouseInsideWindow(Vector2 mousePosition)
    {
        // Get the world space position and size of the window
        Vector2 windowSize = windowRectTransform.rect.size;
        Vector2 windowPosition = windowRectTransform.position;

        // Check if the mouse position is within the bounds of the window ;-;
        return mousePosition.x >= windowPosition.x - windowSize.x / 2 &&
               mousePosition.x <= windowPosition.x + windowSize.x / 2 &&
               mousePosition.y >= windowPosition.y - windowSize.y / 2 &&
               mousePosition.y <= windowPosition.y + windowSize.y / 2;
    }

    private void DrawLineSegment(Vector2 start, Vector2 end)
    {
        // Create a new GameObject to draw the line using LineRenderer
        GameObject lineObject = new GameObject("LineSegment");
        lineObject.transform.SetParent(drawingCanvas.transform);  // Make sure it's a child of the drawing canvas

        // Add LineRenderer component to draw the line
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = Color.black;  // Set the color to black (doesnt work, idk why lmao)
        lineRenderer.endColor = Color.black;
        lineRenderer.positionCount = 2;  // We only need two points (start and end OOP hell yeah)
        lineRenderer.SetPosition(0, start);  // Set the start position
        lineRenderer.SetPosition(1, end);    // Set the end position

        // Set the sorting layer to "Windows Content"
        lineRenderer.sortingLayerName = "Windows";

        // Ensure the line is on top by setting its sorting order (increased to a higher value)
        lineRenderer.sortingOrder = 10;

        // Optional: adjust the Z position to ensure it's drawn in front of everything else (didnt need it, but ill keep it <3)
        lineObject.transform.position = new Vector3(start.x, start.y, drawingCanvas.transform.position.z + 1);
    }

    private void LockWindowDragging(bool lockDragging)
    {
        if (draggableWindow != null)
        {
            draggableWindow.enabled = !lockDragging;  // Disable or enable the DraggableWindow script
        }

        // Ensure the window sorting layer can still be updated when it's not locked
        if (!lockDragging)
        {
            SetSortingLayer();
        }
    }

    private void SetSortingLayer()
    {
        // Ensure that the sorting layer of the window is correctly updated after drawing is complete
        if (draggableWindow != null)
        {
            // You can add logic to reset the sorting order as needed
            draggableWindow.SetSortingLayerRecursively(gameObject, "Windows");
        }
    }
}

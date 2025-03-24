using UnityEngine;

public class DraggableWindow : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private bool isClicked = false; // Track whether the window is clicked
    [SerializeField] private Camera canvasWorldCamera;
    public Canvas canvas;
    private Collider2D windowCollider;

    ///public bool isActive = false;

    void Awake()
    {
        // Initialize canvas and camera
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;

        // Get the Collider component
        windowCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // If the mouse is clicked, check if it's over the object
        if (Input.GetMouseButtonDown(0))
        {
            WindowManager windowmngr = GameObject.Find("Manager").GetComponent<WindowManager>();

            // if the mouse is over this window, mouse is not over the active window (ie. clipping through to this window under)
            if ( (IsMouseOver() && !windowmngr.IsActiveWindowHit()) || (IsMouseOver() && windowmngr.activeWindow == this))
            {
                isClicked = true;
                windowmngr.activeWindow = this;

                if (IsMouseOver() && !windowmngr.IsActiveWindowHit()) // play sfx if moving this to active window
                {
                    GameObject.Find("Manager").GetComponent<AudioManager>().PlayClip("click");
                }

                // Set the parent window and all its children, including child canvases, to the "Windows" sorting layer
                SetSortingLayerRecursively(gameObject, "Windows");

                // IF STATEMENT NEEDED TO FIC MOVING WINDOW BUG
                //if (isActive)
                //{
                    // Start dragging
                    isDragging = true;
                    // Calculate the offset between the object position and mouse position when dragging starts
                    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //}

            }
            else
            {
                if (isClicked)
                {
                    // If clicked outside the window, reset the sorting layer
                    SetSortingLayerRecursively(gameObject, "Default"); // Or any other layer for unselected windows
                    isClicked = false;


                }
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

        // Check if the mouse is over the window's collider
        return windowCollider.bounds.Contains(mousePos);
    }

    // Set the sorting layer of all children recursively, including Canvas components
    public void SetSortingLayerRecursively(GameObject obj, string sortingLayerName)
    {
        // Check and set the sorting layer for the parent object
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = sortingLayerName;
        }

        // If the object has a Canvas component, set its sorting layer as well
        Canvas objCanvas = obj.GetComponentInChildren<Canvas>();
        if (objCanvas != null)
        {
            objCanvas.sortingLayerName = sortingLayerName;
        }

        // Recursively set the sorting layer for all children
        foreach (Transform child in obj.transform)
        {
            SetSortingLayerRecursively(child.gameObject, sortingLayerName);
        }
    }
}

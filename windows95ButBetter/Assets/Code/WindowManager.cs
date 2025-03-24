
using System.Data.Common;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class WindowManager : MonoBehaviour
{
    public DraggableWindow activeWindow;

    void Awake()
    {
       activeWindow = GameObject.Find("DownloadWindow").GetComponent<DraggableWindow>();

    }

    void Update()
    {
        if (activeWindow == null && !GameObject.Find("Manager").GetComponent<LoadManager>().isCrashed)
        {
            activeWindow = GameObject.Find("DownloadWindow").GetComponent<DraggableWindow>();
        }

    }

    public bool IsActiveWindowHit()
    {
        Collider2D collider = activeWindow.gameObject.GetComponent<Collider2D>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Set the z-value to 0 for 2D space

        // Check if the mouse is over the window's collider
        return collider.bounds.Contains(mousePos);
    }

    public void SetActiveWindow(DraggableWindow window)
    {
        activeWindow = window;
    }


}

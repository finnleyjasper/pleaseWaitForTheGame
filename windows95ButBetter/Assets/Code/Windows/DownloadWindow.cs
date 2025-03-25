using UnityEngine;

public class DownloadWindow : DesktopIcon
{
    void Start()
    {
         GameObject.Find("Manager").GetComponent<LoadManager>().isCrashed = false;
    }
    void OnMouseDown()
    {
        if (GameObject.Find("Manager").GetComponent<LoadManager>().isCrashed)
        {
            base.OnMouseDown();
        }
        else
        {
            DraggableWindow downloadWindow = GameObject.Find("DownloadWindow").GetComponent<DraggableWindow>();
            GameObject.Find("Manager").GetComponent<WindowManager>().SetActiveWindow(downloadWindow);
            downloadWindow.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Windows");
        }
    }
}

using UnityEngine;

public class Distraction : MonoBehaviour
{
    public int interactionValue = 0; // see: LoadManager.cs for deets

    public float timePenalty = 1;

    [SerializeField] private GameObject popupWindow;

    public void OpenPopup(LoadManager loadManager)
    {
        // player can only open one window from the same Distraction at a time
        // could make it more realistic and enable them opening many of the same windows but...
        // ...problem for later maybe?
        if (!popupWindow.activeSelf)
        {
            popupWindow.SetActive(true);
            loadManager.AddInteraction(interactionValue, timePenalty);
        }
    }
}

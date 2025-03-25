using UnityEngine;

public class ActivateAndMoveObject : MonoBehaviour
{
    public GameObject targetObject; // Assign the object to be activated in the Inspector

    private void OnMouseDown()
    {
        if (targetObject != null)
        {
            // Activate the target object if it's not active
            if (!targetObject.activeSelf)
            {
                targetObject.SetActive(true);
            }

            // Move the target object to (0,0)
            targetObject.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Target object is not assigned in the Inspector!");
        }
    }
}

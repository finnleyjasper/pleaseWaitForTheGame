using UnityEngine;

public class KillScript : MonoBehaviour
{
    public void Kill()
    {
        GameObject downloadWindow = GameObject.Find("DownloadWindow");
        GameObject errorClone = GameObject.Find("Error(Clone)");

        // Deactivate DownloadWindow instead of destroying it
        if (downloadWindow != null)
        {
            downloadWindow.SetActive(false);
        }

        // Destroy Error(Clone)
        if (errorClone != null)
        {
            Destroy(errorClone);
        }

        Debug.Log("The download wizard was quit");
    }
}

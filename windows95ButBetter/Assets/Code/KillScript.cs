using UnityEngine;

public class KillScript : MonoBehaviour
{
    public void Kill()
    {
        Destroy(GameObject.Find("DownloadWindow"));
        Destroy(GameObject.Find("Error(Clone)"));

        Debug.Log("The download wizard was quit");

    }

}

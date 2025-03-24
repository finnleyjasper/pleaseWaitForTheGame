using UnityEngine;

public class KillScript : MonoBehaviour
{
    public void Kill()
    {
        Application.Quit();
        Debug.Log("The application quit");
    }
}

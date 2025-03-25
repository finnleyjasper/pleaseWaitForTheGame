using UnityEngine;

public class BSOD : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.Quit();
            Debug.Log("The game quit");
        }

    }
}

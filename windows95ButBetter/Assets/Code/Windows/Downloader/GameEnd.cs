using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public AudioClip ErrorSfx; // Assign the sound in the Inspector
    private void Start()
    {
        if (ErrorSfx != null)
        {
            AudioSource.PlayClipAtPoint(ErrorSfx, Camera.main.transform.position);
        }
    }
    // This function is called when the object is clicked
    private void OnMouseDown()
    {
        // Check if the object has a 2D collider and the mouse clicked on it
        if (GetComponent<BoxCollider2D>() != null)
        {
            // Change the scene to "blue"
            SceneManager.LoadScene("blue");
            Debug.Log("Scene changed to 'blue'");
        }
    }
}

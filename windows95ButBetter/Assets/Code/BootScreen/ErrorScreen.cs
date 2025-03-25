using UnityEngine;

public class ErrorScreen : MonoBehaviour
{
    public AudioClip ErrorSfx; // Assign the sound in the Inspector
    public AudioClip YaySfx; // Assign the sound in the Inspector
    public Sprite EasterEgg;   // Assign the Easter Egg sprite in the Inspector

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Play the error sound when the object loads
        if (ErrorSfx != null)
        {
            AudioSource.PlayClipAtPoint(ErrorSfx, Camera.main.transform.position);
        }

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the timer to close the game after 10 seconds
        Invoke("CloseGame", 10f);
    }

    void Update()
    {
        // If the player presses Enter, close the game immediately
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CloseGame();
        }

        // Check for CTRL + ALT + DELETE (technically using LeftControl + LeftAlt + Delete)
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.J))
        {
            if (YaySfx != null)
            {
                AudioSource.PlayClipAtPoint(YaySfx, Camera.main.transform.position);
            }
            // Switch the sprite to EasterEgg if it's assigned
            if (spriteRenderer != null && EasterEgg != null)
            {
                spriteRenderer.sprite = EasterEgg;
            }
        }
    }

    void CloseGame()
    {
        Debug.Log("Game is closing...");
        Application.Quit();
    }
}

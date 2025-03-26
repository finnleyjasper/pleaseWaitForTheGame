using System.Collections;
using UnityEngine;

public class SpriteSwitch : MonoBehaviour
{
    public Sprite[] sprites; // Assign 4 sprites in the Inspector
    public float interval = 20f; // Time before switching

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    void Start()
    {
        if (sprites.Length < 4)
        {
            Debug.LogError("Assign at least 4 sprites in the inspector!");
            return;
        }

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set initial sprite
        spriteRenderer.sprite = sprites[currentSpriteIndex];

        // Start the sprite change routine
        StartCoroutine(ChangeSpriteRoutine());
    }

    IEnumerator ChangeSpriteRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Switch to the next sprite
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }
}

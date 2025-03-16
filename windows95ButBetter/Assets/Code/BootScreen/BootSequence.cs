using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSequence : MonoBehaviour
{
    private float timer = 0f;
    private bool hasDeletedObject = false;
    public AudioClip sfx_load; 
    private AudioSource audioSource;

    void Start()
    {
        // Create an AudioSource (Idk what i'll do here later)
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // At 8 seconds, delete Boot Screen & Play the audio
        if (timer >= 8f && !hasDeletedObject)
        {
            GameObject bootObject = GameObject.Find("Boot_0");
            if (bootObject != null)
            {
                Destroy(bootObject);
            }

            if (sfx_load != null)
            {
                audioSource.PlayOneShot(sfx_load);
            }

            hasDeletedObject = true; // Prevent repeated deletion
        }

        // load the 'Main' scene
        if (timer >= 15f)
        {
            SceneManager.LoadScene("Main");
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    // this is the worst possible way to do this but whatever
    public AudioClip loadComplete;
    public AudioClip click;
    public AudioClip error;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClip(string clipName)
    {
        // cant be fucked figuring out how dictionaries work with unity this will do the job
        switch (clipName)
        {
            case "complete":
                audioSource.clip = loadComplete;
            break;
            case "click":
                audioSource.clip = click;
            break;
                case "error":
                audioSource.clip = error;
            break;
        }

        audioSource.Play();
    }
}

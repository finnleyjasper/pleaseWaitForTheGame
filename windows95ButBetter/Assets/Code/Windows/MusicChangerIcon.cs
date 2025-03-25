using UnityEngine;

public class MusicChangerIcon : DesktopIcon
{
    public AudioSource musicPlayer; // Assign an AudioSource component in Inspector
    public AudioClip[] musicTracks; // Assign multiple tracks in Inspector
    private int currentTrackIndex = 0;

    override public void OnMouseDown()
    {
        // Handle double-click detection
        if (Time.time - lastClickTime < doubleClickTime)
        {
            // Double-click detected, change the music
            ChangeMusic();
            GameObject.Find("Manager").GetComponent<AudioManager>().PlayClip("click");
        }
        else
        {
            // First click, spawn selection overlay
            if (activeOverlay == null && SelectionOverlayPrefab != null)
            {
                activeOverlay = Instantiate(SelectionOverlayPrefab, transform.position, Quaternion.identity, transform);
                Debug.Log("Overlay created on icon click");
                DesktopManager.Instance.RegisterHighlightObject(activeOverlay);
            }
        }

        lastClickTime = Time.time;
    }

    void ChangeMusic()
    {
        if (musicPlayer != null && musicTracks.Length > 0)
        {
            // Stop the current track if playing
            if (musicPlayer.isPlaying)
            {
                musicPlayer.Stop();
            }

            // Cycle to the next track
            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
            musicPlayer.clip = musicTracks[currentTrackIndex];
            musicPlayer.Play();

            Debug.Log("Playing track: " + musicPlayer.clip.name);
        }
        else
        {
            Debug.LogWarning("Music player or musicTracks array is not set!");
        }
    }
}

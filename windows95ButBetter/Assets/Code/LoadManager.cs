using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private int CrashThreshold;
    private int interactionScore = 0;
    [SerializeField] private DraggableWindow loadWindow;
    [SerializeField] private Timer timer;
    private int textIndex = 0;
    [SerializeField] private TextMeshProUGUI textAsset;

    private string[] dialogueOptions = {
        "hey hold up what are you doing",
        "bro it's only like a minute???",
        "WAIT WAIT WAIT JU-",
        "you have the attention span of a toddler!!!",
        "LOOK AT ME!!!!!",
        "hey wait a second okay just hold on-",
        "seriously don't press anything else i'm trying my best",
    };

    public Texture2D cursorClick;
    public Texture2D cursorPoint;
    public GameObject secret;
    [SerializeField] private GameObject errorPrefab;
    [SerializeField] private GameObject gameMovieWindowPrefab; // New field for GameMovieWindow prefab

    public bool isCrashed = false;

    void Start()
    {
        ResetCrashThreshold();
        CursorReset();
    }

    void Update()
    {
        // If DownloadWindow is disabled, force isCrashed to false
        GameObject downloadWindow = GameObject.Find("DownloadWindow");
        if (downloadWindow != null && !downloadWindow.activeSelf)
        {
            isCrashed = false;
            Debug.Log("DownloadWindow is disabled, resetting crash state.");
        }

        // If crashed but interaction score has increased past the new threshold, reset crash state
        if (isCrashed && interactionScore < CrashThreshold)
        {
            isCrashed = false;
            Debug.Log("Interaction score has increased past the new threshold, resetting crash state.");
        }

        // Check for a crash condition
        if (!isCrashed && (interactionScore >= CrashThreshold || timer.timeRemaining > 300))
        {
            HandleCrash();
        }
    }

    void HandleCrash()
    {
        isCrashed = true;
        interactionScore = 0; // Reset score after crash
        CrashThreshold = UnityEngine.Random.Range(6, 12);
        Debug.Log("The wizard crashed. New threshold set to: " + CrashThreshold);

        // Play error sound and instantiate error
        GameObject.Find("Manager").GetComponent<AudioManager>().PlayClip("error");
        Instantiate(errorPrefab, loadWindow.gameObject.transform.position, Quaternion.identity);
    }

    void ResetAfterCrash()
    {
        isCrashed = false; // Allow future crashes
        ResetCrashThreshold(); // Set a new random threshold
        interactionScore = 0; // Fully reset interaction score again
        Debug.Log("Crash Reset! New Threshold: " + CrashThreshold);
    }

    void ResetCrashThreshold()
    {
        CrashThreshold = UnityEngine.Random.Range(6, 12);
        Debug.Log("New Crash Threshold set: " + CrashThreshold);
    }

    public void AddInteraction(int interactionValue)
    {
        if (isCrashed || timer.loadComplete) return; // Ignore interactions during crash

        interactionScore += interactionValue;
        textAsset.text = dialogueOptions[Mathf.Min(textIndex, dialogueOptions.Length - 1)];
        textIndex++;

        Debug.Log("Current interaction score: " + interactionScore);
    }

    public void LoadComplete()
    {
        gameObject.GetComponent<AudioManager>().PlayClip("completed");
        textAsset.text = "Game loaded! Thanks for playing :)";

        // Load the "GameMovie" scene instead of instantiating objects
        SceneManager.LoadScene("GameMovie");

        WindowManager windowmngr = GameObject.Find("Manager").GetComponent<WindowManager>();
        windowmngr.SetActiveWindow(loadWindow);
    }

    public void CursorClick()
    {
        UnityEngine.Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void CursorReset()
    {
        UnityEngine.Cursor.SetCursor(cursorPoint, Vector2.zero, CursorMode.ForceSoftware);
    }
}

/*
*   Manages communication between "distractions" and the timer/loading screen itself
*
*   Will check how many things the player has interacted with and control:
*       -> Load screen crash (too many things interacted with)
*       -> Load the video (player didn't touch anything)
*       -> New dialogue to show on the load screen
*
*/

using System.Collections;
using System.Data.Common;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class LoadManager : MonoBehaviour
{
    [SerializeField] public int CrashThreshold = 8;

    // when the interactionScore reaches the CrashThreshold a crash will occur
    // this is increased by interacting with Distractions, which can each different amounts (their interactionValue)
    private int interactionScore = 0;
    [SerializeField] private DraggableWindow loadWindow;

    [SerializeField] private Timer timer;

    private int textIndex = 0;

    [SerializeField] public TextMeshProUGUI textAsset;

    private string[] dialogueOptions = {
        "hey hold up what are you doing",
        "bro it's only like a minute???",
        "you have the attention span of a toddler!!!",
        "LOOK AT ME!!!!!",
        "hey wait a second okay just hold on-",
        "seriously don't press anything else i'm trying my best",
        "WAIT WAIT WAIT JU-"
        };

    public Texture2D cursorClick;
    public Texture2D cursorPoint;

    public bool isCrashed; // this is only used in windowmanager just accept that i needed it

    [SerializeField] private GameObject errorWindow;

    void Start()
    {
        // for some fucking reason the cursor is huge if this isnt manullalatllty done
        CursorReset();
    }
    // Update is called once per frame
    void Update()
    {
        if (interactionScore >= CrashThreshold || timer.timeRemaining > 300) // load time greater than 5 mins
        {
            //textAsset.text = "FATAL ERROR: The program crashed before it could load.";
            GameObject.Find("Manager").GetComponent<AudioManager>().PlayClip("error");
            gameObject.GetComponent<WindowManager>().activeWindow = GameObject.Find("Manager").GetComponent<DraggableWindow>();
            timer.crashed = true;

            errorWindow.SetActive(true);

            isCrashed = true;
            //loadWindow.gameObject.SetActive(false);
            interactionScore = 0;

        }

    }

    public void AddInteraction(int interactionValue)
    {
        interactionScore += interactionValue;
        textAsset.text = dialogueOptions[textIndex];
        textIndex += 1;

        Debug.Log("Current interaction score: " + interactionScore);
    }

    public void LoadComplete()
    {
        //DO STUFF
        gameObject.GetComponent<AudioManager>().PlayClip("completed");
        textAsset.text = "Game loaded! Please wait a moment while we load your content...";

        // bring the load window to the front
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

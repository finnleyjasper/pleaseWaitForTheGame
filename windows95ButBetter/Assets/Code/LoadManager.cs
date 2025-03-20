/*
*   Manages communication between "distractions" and the timer/loading screen itself
*
*   Will check how many things the player has interacted with and control:
*       -> Load screen crash (too many things interacted with)
*       -> Load the video (player didn't touch anything)
*       -> New dialogue to show on the load screen
*
*/

using System.Data.Common;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class LoadManager : MonoBehaviour
{
    [SerializeField] public const int CrashThreshold = 10;

    // when the interactionScore reaches the CrashThreshold a crash will occur
    // this is increased by interacting with Distractions, which can each different amounts (their interactionValue)
    private int interactionScore = 0;
    [SerializeField] private DraggableWindow loadWindow;

    [SerializeField] private Timer timer;

    private int textIndex = 0;

    [SerializeField] public TextMeshProUGUI textAsset;

    private string[] dialogueOptions = {
        "aa",
        "stop",
        "pls",
        "omg"
        };

    // Update is called once per frame
    void Update()
    {
        if (interactionScore >= CrashThreshold)
        {

            loadWindow.gameObject.SetActive(false);
            interactionScore = 0;

            Debug.Log("The program crashed before it could load.");
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
        textAsset.text = "Game loaded! Please wait a moment while we load your content...";
    }

}

/*
*   Manages communication between "distractions" and the timer/loading screen itself
*
*   Will check how many things the player has interacted with and control:
*       -> Load screen crash (too many things interacted with)
*       -> Load the video (player didn't touch anything)
*       -> New dialogue to show on the load screen
*
*/

using UnityEngine;


public class LoadManager : MonoBehaviour
{
    public const int CrashThreshold = 10;

    // when the interactionScore reaches the CrashThreshold a crash will occur
    // this is increased by interacting with Distractions, which can each different amounts (their interactionValue)
    private int interactionScore = 0;
    [SerializeField] private DraggableWindow loadWindow;

    [SerializeField] private Timer timer;

    // Update is called once per frame
    void Update()
    {
        if (interactionScore >= CrashThreshold)
        {
            // window should crash

            loadWindow.gameObject.SetActive(false);
            interactionScore = 0;

            Debug.Log("The program crashed before it could load.");
        }

    }

    public void AddInteraction(int interactionValue, float timePenalty)
    {
        interactionScore += interactionValue;
        timer.AddTime(timePenalty);

        Debug.Log("Current interaction score: " + interactionScore);
    }
}

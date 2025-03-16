/*
*   Manages communication between "distractions" and the timer/loading screen itself
*
*   Will check how many things the player has interacted with and control:
*       -> Load screen crash (too many things interacted with)
*       -> Load the video (player didn't touch anything)
*       -> New dialogue to show on the load screen
*
*   For now crash is determined only by the *number* of things interacted with, but we could
*   could change this to a "points" based system - ie. watching a video adds A time and contributes
*   to a crash B amount, while reading a text file only adds X amount of time and contributes Y amount
*
*/

using UnityEngine;


public class LoadManager : MonoBehaviour
{
    public const int CrashThreshold = 10;

    private int interactions = 0;

    [SerializeField] private Timer timer;

    // Update is called once per frame
    void Update()
    {
        if (interactions >= CrashThreshold)
        {
            // window should crash

            timer.Reset();
            interactions = 0;

            Debug.Log("The program crashed before it could load.");
        }

    }

    public void AddInteraction(int interactionValue, float timePenalty)
    {
        interactions += interactionValue;
        timer.AddTime(timePenalty);

        Debug.Log("Current interactions: " + interactions);
    }
}

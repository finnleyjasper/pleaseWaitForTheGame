using UnityEngine;

public class Distractor : MonoBehaviour
{
    // The amount of time to add every second
    public float timeToAddPerSecond = 1f;

    // Whether we should add a fixed amount to Default Time
    public bool addFixedAmount = false;
    public float fixedAmount = 10f;

    // Reference to the Timer script on the NewTimer GameObject
    private Timer timerScript;

    // Flag to track if the fixed amount has already been added
    private bool fixedAmountAdded = false;

    void Start()
    {
        // Find the Timer script attached to the NewTimer GameObject
        GameObject newTimer = GameObject.Find("NewTimer");

        if (newTimer != null)
        {
            timerScript = newTimer.GetComponent<Timer>();
        }
        else
        {
            Debug.LogError("NewTimer GameObject not found in the scene!");
        }

        // Start adding time every second to the timer's timeRemaining
        if (timerScript != null)
        {
            // Start adding time every second
            InvokeRepeating("AddTime", 0f, 1f);
        }
    }

    void AddTime()
    {
        // If the Timer script exists and the Default Time is not null, update timeRemaining
        if (timerScript != null && timerScript.IsComplete == false)
        {
            if (addFixedAmount && !fixedAmountAdded)
            {
                // Add the fixed amount of time once
                timerScript.AddTime(fixedAmount);
                fixedAmountAdded = true;  // Ensure it doesn't get added again
            }
            else if (!addFixedAmount)
            {
                // Add 1 second to timeRemaining every second
                timerScript.AddTime(timeToAddPerSecond);
            }
        }
    }

    // Optional: If you want to stop the distraction
    public void StopDistraction()
    {
        CancelInvoke("AddTime");
    }
}

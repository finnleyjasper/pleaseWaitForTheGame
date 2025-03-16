/*
*   Contains the progress bar and text.
*
*   Reference this object's IsCompelete property when choosing to load the next thing
*
*   timeRemaining is a public field that can be changed directly by other objects
*/

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool loadComplete = false; // access through property

    [SerializeField] private float defaultTime = 60; //

    private float timeRemaining; // 60 seconds is the default - the player can cause this to increase by interacting w/ stuff

    private float lastSampledTime = 0;

   [SerializeField] private RectTransform loadBar; // the bar that will increase with time

   [SerializeField] private TextMeshProUGUI numberTextToUpdate; // the actual text that gets rendered to the screen

    private float loadBarIncrememnt; // the amount the bar should tick up each second

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (!loadComplete)
        {
            // update the time and bar
            if (Time.time > (lastSampledTime + 1))
            {
                lastSampledTime = Time.time;
                timeRemaining -= 1;
                loadBar.sizeDelta = new Vector2((loadBar.rect.width + loadBarIncrememnt), loadBar.rect.height);
            }

            // render the text
            double minutes = Math.Truncate(timeRemaining / 60);
            double seconds = timeRemaining - (60 * minutes);

            if (minutes >= 10)
            {
                numberTextToUpdate.text = "Time remaining: " + minutes + ":";
            }
            else
            {
                numberTextToUpdate.text = "Time remaining: 0" + minutes + ":";
            }

            if (seconds < 10)
            {
                numberTextToUpdate.text = numberTextToUpdate.text + "0" + seconds;
            }
            else
            {
                numberTextToUpdate.text = numberTextToUpdate.text + seconds;
            }
        }

        // checks if done
        if (timeRemaining <= 0)
        {
            loadComplete = true;
            Debug.LogWarning("Well done! You waited!");
            this.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        timeRemaining = defaultTime;
        loadComplete = false;

        // reset bar to 0%
        loadBar.sizeDelta = new Vector2(0, loadBar.rect.height);
        CalculateLoadIncrement();
    }

    public void AddTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
        CalculateLoadIncrement();
    }

    private void CalculateLoadIncrement()
    {
        // finding the increment - this will change depending on the size of the bar nad how much time is left
        RectTransform background = GetComponent<RectTransform>();
        float maxLoadBarSize = background.rect.width - 5;
        float loadBarSizeRemaining = maxLoadBarSize - loadBar.rect.width;
        loadBarIncrememnt = loadBarSizeRemaining / timeRemaining;
    }

    // property so other things in the scene can do stuff when the load is complete... like minecraft movie...
    public bool IsComplete
    {
         get { return loadComplete; }
    }
}

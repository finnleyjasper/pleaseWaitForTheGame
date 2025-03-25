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
using UnityEngine.SocialPlatforms;

public class Timer : MonoBehaviour
{
    public bool loadComplete = false; // access through property

    [SerializeField] public float defaultTime = 60; //

    public float timeRemaining; // 60 seconds is the default - the player can cause this to increase by interacting w/ stuff

    private float lastSampledTime = 0;

   [SerializeField] private Transform loadBar; // the bar that will increase with time

   [SerializeField] private TextMeshProUGUI numberTextToUpdate; // the actual text that gets rendered to the screen

    private float loadBarIncrememnt; // the amount the bar should tick up each second

    // The maximum scale multiplier (final scale: 31.47341x the original width)
    public float maxScaleMultiplier = 31.47341f;

    public bool crashed = false;

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
                loadBar.localScale = new Vector2(loadBar.localScale.x + loadBarIncrememnt, loadBar.localScale.y);
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
        else if (crashed)
        {
           this.gameObject.SetActive(false);
        }

        // checks if done
        if (timeRemaining <= 0)
        {
            loadComplete = true;
            Debug.LogWarning("Well done! You waited!");

            LoadManager manager = GameObject.Find("Manager").GetComponent<LoadManager>();
            manager.LoadComplete();

            this.gameObject.SetActive(false);
        }

    }

    public void Reset()
    {
        timeRemaining = defaultTime;
        loadComplete = false;

        // reset bar to 0%
        loadBar.localScale = new Vector2(0, loadBar.localScale.y);
        CalculateLoadIncrement();
    }

    public void AddTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
        CalculateLoadIncrement();
    }

    private void CalculateLoadIncrement()
    {
        float loadBarSizeRemaining = maxScaleMultiplier - loadBar.localScale.x;
        loadBarIncrememnt = loadBarSizeRemaining / timeRemaining;
    }

    // property so other things in the scene can do stuff when the load is complete... like minecraft movie...
    public bool IsComplete
    {
         get { return loadComplete; }
    }

}

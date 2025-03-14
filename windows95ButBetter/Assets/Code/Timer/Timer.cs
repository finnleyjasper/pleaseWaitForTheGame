/*
*   Contains the progress bar and text.
*
*   Reference this object's IsCompelete property when choosing to load the next thing
*
*   timeRemaining is a public field that can be changed directly by other objects
*/

using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool loadComplete = false; // access through property

    public float timeRemaining = 60; // 60 seconds is the default - the player can cause this to increase by interacting w/ stuff

    private float lastSampledTime = 0;

   [SerializeField] private RectTransform loadBar; // the bar that will increase with time

   [SerializeField] private TextMeshProUGUI numberTextToUpdate; // the actual text that gets rendered to the screen

    private float loadBarIncrememnt; // the amount the bar should tick up each second

    void Start()
    {
        // reset bar tp 0%
        loadBar.sizeDelta = new Vector2(0, loadBar.rect.height);

        // finding the increment - this will change depending on the size of the bar
        RectTransform background = GetComponent<RectTransform>();
        float maxLoadBarSize = background.rect.width - 5;
        loadBarIncrememnt = maxLoadBarSize / timeRemaining;

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
            if (timeRemaining == 60)
            {
                numberTextToUpdate.text = "Time remaining: 01:00";
            }
            else
            {
                numberTextToUpdate.text = "Time remaining: 00:" + timeRemaining.ToString();
            }

        }

        // checks if done
        if (timeRemaining <= 0)
        {
            loadComplete = true;
            this.gameObject.SetActive(false);
        }
    }

    // property so other things in the scene can do stuff when the load is complete... like minecraft movie...
    public bool IsComplete
    {
         get { return loadComplete; }
    }

}

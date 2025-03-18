using UnityEngine;
using TMPro;
using System;

public class Windows95Clock : MonoBehaviour
{
    public TextMeshProUGUI TimeText; // Assign in Inspector

    void Update()
    {
        if (TimeText != null)
        {
            TimeText.text = DateTime.Now.ToString("h:mm tt"); // Windows 95 format (e.g., "3:45 PM")
        }
    }
}

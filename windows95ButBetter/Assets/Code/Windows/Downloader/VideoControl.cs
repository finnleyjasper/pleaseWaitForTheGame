using UnityEngine;
using System.Collections;  // <-- Add this line

public class CrashAfterTime : MonoBehaviour
{
    public GameObject crashPrefab; // Reference to the CrashPrefab to instantiate
    private float waitTime = 35f;   // Time to wait before instantiating the prefab

    void Start()
    {
        // Start the coroutine to wait for the specified time
        StartCoroutine(InstantiateCrashPrefabAfterTime());
    }

    // Coroutine to wait for 'waitTime' seconds
    IEnumerator InstantiateCrashPrefabAfterTime()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Instantiate the CrashPrefab
        if (crashPrefab != null)
        {
            Instantiate(crashPrefab, transform.position, Quaternion.identity);
            Debug.Log("CrashPrefab instantiated after " + waitTime + " seconds.");
        }
        else
        {
            Debug.LogError("CrashPrefab is not assigned!");
        }
    }
}

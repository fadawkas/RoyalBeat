using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("WaypointIndex", 0);
        PlayerPrefs.SetInt("HealthScore", 30);
        PlayerPrefs.SetInt("Level1Completed", 0);
        PlayerPrefs.SetInt("Level2Completed", 0);
        PlayerPrefs.Save();
    }
}

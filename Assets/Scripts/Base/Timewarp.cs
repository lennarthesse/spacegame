using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timewarp : MonoBehaviour
{
    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            if (Time.timeScale > 0) {
                Time.timeScale -= 1;
                Debug.Log("Timewarp: " + Time.timeScale);
            }
        } else if (Input.GetKeyDown(KeyCode.Period)) {
            if (Time.timeScale < 10) {
                Time.timeScale += 1;
                Debug.Log("Timewarp: " + Time.timeScale);
            }
        }

        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }
}

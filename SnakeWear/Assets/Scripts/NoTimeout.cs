using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTimeout : MonoBehaviour {

    void Awake()
    {
        // make the device never sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltZ : MonoBehaviour {
    Text TiltTextZ;
	// Use this for initialization
    //TESTING PURPOSES CAN BE DELETED
	void Start () {
        TiltTextZ = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.deviceOrientation == DeviceOrientation.FaceUp)
        {
          float y = Input.acceleration.y;
          TiltTextZ.text = "Y axis: " + y.ToString();
        }
        
    }
}

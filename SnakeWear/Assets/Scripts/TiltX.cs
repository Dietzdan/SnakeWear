using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltX : MonoBehaviour {
        Text TiltTextX;
    //TESTING PURPOSE CAN BE DELETED
	// Use this for initialization
	void Start () {
        TiltTextX = GetComponent<Text>();
        
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.acceleration.x;
        TiltTextX.text = "X axis: " + x.ToString();
	}
}

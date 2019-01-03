using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TiltButton : MonoBehaviour {
    Button TiltMode;
	// Use this for initialization
	void Start () {
        //go to tilt mode when button is pressed
        TiltMode = GetComponent<Button>();
        TiltMode.onClick.AddListener(ToTiltScene);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void ToTiltScene()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }
}

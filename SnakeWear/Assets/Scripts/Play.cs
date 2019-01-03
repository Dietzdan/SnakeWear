using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Play : MonoBehaviour {
    Button Playbutton;
    
	// Use this for initialization
	void Start () {
        //load the play when button is pressed
        Playbutton = GetComponent<Button>();
        Playbutton.onClick.AddListener(loadLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void loadLevel() {

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    
 
}

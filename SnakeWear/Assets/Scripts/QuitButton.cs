using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;// delete when building

public class QuitButton : MonoBehaviour {

 
    Button quit;
	// Use this for initialization
	void Start () {
        //quit when buton is pressed
        quit = GetComponent<Button>();
        quit.onClick.AddListener(QuitGame);
	}
	
	// Update is called once per frame
	void Update () {

    
    }

    void QuitGame()
    {
       // EditorApplication.isPlaying = false;//delete when building 
        Application.Quit();
    }   
}

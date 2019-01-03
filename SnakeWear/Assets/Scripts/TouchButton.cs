using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchButton : MonoBehaviour {
    Button TouchGame;
	// Use this for initialization
	void Start () {
        //buttom to go to touch mode scene
        TouchGame = GetComponent<Button>();
        TouchGame.onClick.AddListener(ToTouchScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ToTouchScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    

}

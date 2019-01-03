using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    Button menu;
    // Use this for initialization
    void Start () {
        // load main menu when button is pressed 
        menu = GetComponent<Button>();
        menu.onClick.AddListener(Menu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Menu()
    {

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}

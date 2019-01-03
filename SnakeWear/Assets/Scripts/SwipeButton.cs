using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwipeButton : MonoBehaviour {
    Button SwipeMode;
	// Use this for initialization
	void Start () {
        SwipeMode = GetComponent<Button>();
        SwipeMode.onClick.AddListener(ToSwipeScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ToSwipeScene()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}

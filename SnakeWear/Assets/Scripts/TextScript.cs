using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class TextScript : MonoBehaviour {

    public GameObject Food;
    Snake S;
    public static int turns;

  public static Stopwatch Timer = new Stopwatch();
    Scene CurrentScene; 
    string SceneName;
    Text ScoreText;
    public Text TimeText;
    public Text TurnText;
    bool ZeroScore;
    bool FoodAppeared;
    bool TimerReset;
   
    public static int ScoreNum = 0;
	// Use this for initialization
	void Start () {
        ScoreText = GetComponent<Text>();
       
        


    }

    // Update is called once per frame
    void FixedUpdate() {
       
        //find food in the scene
        if (GameObject.Find("Food(Clone)") != null)
        {
            //use the bool so the score only goes up once while food off screen
            FoodAppeared = true;
        }

        CurrentScene = SceneManager.GetActiveScene();
        SceneName = CurrentScene.name;
 //switch to check which control mode we're in 
        switch (SceneName)
        {//this is used to prevent a bug where the player would get two points instead of one
            //when not in touch mode
            case "Snake":
                S = (Snake)GameObject.Find("Snakehead").GetComponent("Snake");
                turns = S.TurnsCounter;
                if (TimerReset == false)
                {
                    Timer.Reset();
                    TimerReset = true;
                    Timer.Start();
                }

                
                if (ZeroScore == false)
                {

                    ScoreNum = 0;
                    ZeroScore = true;
                }
                if (GameObject.Find("Food(Clone)") == null)
                {
                    ScoreNum += 1;
                }
                ScoreText.text = "Score: " + ScoreNum.ToString();
        
                break;
                
            case "SnakeSwipe":
                S = (Snake)GameObject.Find("Snakehead").GetComponent("Snake");
                turns = S.TurnsCounter;
                if (TimerReset == false)
                {
                    Timer.Reset();
                    TimerReset = true;
                    Timer.Start();
                }
                
              
                if (ZeroScore == false)
                {

                    ScoreNum = 0;
                    ZeroScore = true;
                }
                if (FoodAppeared == true)
                {
                    if (GameObject.Find("Food(Clone)") == null)
                    {
                        ScoreNum++;
                        FoodAppeared = false;
                    }
                }
                ScoreText.text = "Score: " + ScoreNum.ToString();
            
                break;
            case "SnakeTilt":
                S = (Snake)GameObject.Find("Snakehead").GetComponent("Snake");
                turns = S.TurnsCounter;
                if (TimerReset == false)
                {
                    Timer.Reset();
                    TimerReset = true;
                    Timer.Start();
                }
              
             
                if (ZeroScore == false)
                {

                    ScoreNum = 0;
                    ZeroScore = true;
                }
                if (FoodAppeared == true)
                {
                    if (GameObject.Find("Food(Clone)") == null)
                    {
                        ScoreNum++;
                        
                        FoodAppeared = false;
                    }
                }
                ScoreText.text = "Score: " + ScoreNum.ToString();
                
                break;
            case "GameOver":
                Timer.Stop();
               
                ScoreText.text = "Final Score: " + ScoreNum.ToString();
                TimeText.text = "Time: " + Timer.Elapsed.TotalSeconds +" Seconds";
                TurnText.text = "Turns: " + turns;
                ZeroScore = false;
                TimerReset = false;
                
                break;

            default:
                break;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

    public GameObject Food;
    bool FoodonScreen;
    public Transform BorderTop;
    public Transform BorderBottom;
    public Transform BorderLeft;
    public Transform BorderRight;
    List<Vector2> SnakePos = new List<Vector2>();

    // Use this for initialization
    void Start () {
        //if you want multiple food on screen at once use this without the update function
        //InvokeRepeating("Spawn", 3, 5);
        FoodonScreen = false;
        
	}
    //if you want individual spawn one after the other use the update function CHOSEN
    private void FixedUpdate()
    {
        //get snake head object
        Snake S = (Snake)GameObject.Find("Snakehead").GetComponent<Snake>();
      
        for(int x = 0;x<S.tail.Count;x++)
        {
            //filling the snake pos vector with the positon of teh tails aswell
            SnakePos.Insert(0,S.tail[x].position);
        }
        Vector2 HeadVec = GameObject.Find("Snakehead").GetComponent<Transform>().position;
        //adding the head to stop the food from spawning on to of the snake
        SnakePos.Insert(0,HeadVec);
        //bool to check if food is already on screen if not spawn one
        if (FoodonScreen == false)
        {
            Spawn();
            FoodonScreen = true;
        }
        else if (GameObject.Find("Food(Clone)") == null)
        {
           FoodonScreen = false;
        }
        //clear snakepos and re fill each time 
        SnakePos.Clear();
       
    }
    //food spawn command
    void Spawn()
    {
            bool Overlap = true;
            int counter = 0;
            
            Vector2 FoodVec;
            
            int x = (int)Random.Range(BorderLeft.position.x , BorderRight.position.x );

            int y = (int)Random.Range(BorderTop.position.y , BorderBottom.position.y );
            FoodVec = new Vector2(x, y);
        while (Overlap == true)
        {
            for(int i =0;i<SnakePos.Count;i++)
            {
                if(FoodVec == SnakePos[i])
                {
                     x = (int)Random.Range(BorderLeft.position.x, BorderRight.position.x);
                     y = (int)Random.Range(BorderTop.position.y, BorderBottom.position.y);
                    FoodVec = new Vector2(x, y);
                }

            }

            if(counter == 1)
            {
                Overlap = false;
                
            }
            counter++;
        }
        Instantiate(Food, new Vector2(x, y), Quaternion.identity);

       
        

       
    }
}

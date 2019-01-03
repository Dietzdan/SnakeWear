using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Snake : MonoBehaviour {
    float x = 0.25f;
    float y = 0.25f;
    int time = 0;
    int Timestep = 15;
    public int TurnsCounter;
    //Touch control buttons
    public Button Downbutton;
    public Button Upbutton;
    public Button LeftButton;
    public Button RightButton;
    // Swipe Control Buttons
    bool tap;
    bool isDragging = false;
    bool SwipeMode = false;

    //tilt Controls
    bool TiltMode = false;
    bool faceup;
    Vector2 StartTouch, SwipeDelta;

    Vector2 Right;
    Vector2 Left;
    Vector2 Up;
    Vector2 Down;
    Vector2 Direction;
    //Tail list
   public List<Transform> tail = new List<Transform>();
    //bool to see if ate food
    bool ate = false;

    public GameObject tailPrefab;
    public GameObject HeadPrefab;
    //float o do position calculations 
    float Gridx;
    float Gridy;
    //int to pass to the array 
    int IGridx;
    int IGridy;

    //getting current scene
    Scene CurrentScene;

    // Use this for initialization
    void Start () {

        CurrentScene = SceneManager.GetActiveScene();
        //old method of movement using invoke repeating
        // InvokeRepeating("Move", 0.3f, 0.3f);
        Right.x = x;
        Left.x = -x;
        Up.y = y;
        Down.y = -y;
        TurnsCounter = 0;
        //if the mode choosen is touch add all the buttons and create on click events
        if (CurrentScene.name == "Snake")
        {
            Downbutton.GetComponent<Button>();
            Downbutton.onClick.AddListener(DirDown);
            Upbutton.GetComponent<Button>();
            Upbutton.onClick.AddListener(DirUp);
            LeftButton.GetComponent<Button>();
            LeftButton.onClick.AddListener(DirLeft);
            RightButton.GetComponent<Button>();
            RightButton.onClick.AddListener(DirRight);
        }
        //else bool to checkwhich mode were in
        else if (CurrentScene.name =="SnakeSwipe" )
        {
            SwipeMode = true;
        }
        else if (CurrentScene.name == "SnakeTilt")
        {
            TiltMode = true;
        }
       
        //spawn the snake head and two tails parts using the grid 
            MapGrid MG = (MapGrid)GameObject.Find("MainCamera").GetComponent("MapGrid");
        this.transform.position = MG.PlayArea[5, 5];

        GameObject g = (GameObject)Instantiate(tailPrefab, MG.PlayArea[5, 4], Quaternion.identity);
        tail.Insert(0, g.transform);
        GameObject g2 = (GameObject)Instantiate(tailPrefab, MG.PlayArea[5, 3], Quaternion.identity);
        tail.Insert(1, g2.transform);
        //snkae will always start moving down
        Direction = Down;



    }
    

    void FixedUpdate () {
        time++;
        //check if swipe mode is active before doing the swipe movement calculations      
        if (SwipeMode == true)
        {
           
            //reseting the swipe bools
            tap = false;
           
            //logic for swipe inputs for Computer testing purposes
            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                isDragging = true;
                StartTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Reset();
            }

            //swipe logic for mobile/watch
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDragging = true;
                    StartTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                    Reset();
                }
            }

            //calautingh the distance of the swipe
            SwipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touches.Length > 0)
                {
                    SwipeDelta = Input.touches[0].position - StartTouch;
                }
                else if (Input.GetMouseButton(0))
                {
                    SwipeDelta = (Vector2)Input.mousePosition - StartTouch;
                }
            }
            
            //checking if the deadzone has been crossed before a swipe is detected
            //65 seems to be the best PLAYTESTED
            if (SwipeDelta.magnitude > 65)
            {
                //checking direction
                float SwipeX = SwipeDelta.x;
                float SwipeY = SwipeDelta.y;
                //your either swiping left or right
                if (Mathf.Abs(SwipeX) > Mathf.Abs(SwipeY))
                {
                    if (SwipeX < 0)
                    {
                        DirLeft();
                    }
                    else
                    {
                        DirRight();
                    }
                }
                //swiping up or down
                else
                {
                    if (SwipeY < 0)
                    {

                        DirDown();
                    }
                    else
                    {
                        DirUp();
                    }
                }

                Reset();
            }
        }
        //tilt mode check
       if (TiltMode == true)
        {
           // get teh accelerometer numbers
            Vector3 tilt = Input.acceleration;
         
           // check if device is face up before doing next input
            if (tilt.x <= 0.45 && tilt.x >= -0.45 && tilt.y>= -0.45 && tilt.y<=0.45)
            {
                faceup = true;
               
            }
           //checks the direction of the tilt then sets faceup to false
           //device must return to orginal postion to do a new input  
           //Note: 4 is too sensitive PLAYTESTED, 5 seems good 
           //Note:Dont Flick wrist causes snake to go in wrong direction
            if (faceup == true && tilt.x > 0.45)
            {
                DirRight();
                faceup = false;
            }
            else if (faceup == true && tilt.x < -0.45)
            {
                DirLeft();
                faceup = false;
            }
            else if (faceup == true && tilt.y > 0.45)
            {
                DirUp();
                faceup = false;
            }
            else if (faceup == true && tilt.y < -0.45)
            {
                DirDown();
                faceup = false;
            }
        }
      
        if(time>= Timestep)
        {
            Move();
           
           time = 0;
        }      
    }
   
    //move command
    private void Move(){

        MapGrid MG = (MapGrid)GameObject.Find("MainCamera").GetComponent("MapGrid");

        //Getting the current pos 
        Vector2 GridPos = FindPosition(transform.position); 
        Vector2 Pos = MG.PlayArea[(int)GridPos.x,(int)GridPos.y];

       
        
        //Setting move to the direction
        transform.Translate(Direction);
        //if the snkae has eaten a piece add a tail piece to the end of the snake 
        if (ate)
        {
           
            GameObject g = (GameObject)Instantiate(tailPrefab, Pos, Quaternion.identity);

            tail.Insert(0, g.transform);

            ate = false;
         
        }

        else if(tail.Count>0)
        {
            
            tail.Last().position = Pos;

            //Add one to front remove one from back of list 
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);


        }
    }
    //direction commands make it easier
  private void DirDown()
    {
        if (Direction != Up)
        {
            Direction = Down;
            TurnsCounter++;
        }
       
    }

    private void DirUp()
    {
        if (Direction != Down)
        {
            Direction = Up;
            TurnsCounter++;
        }
    }

    private void DirLeft()
    {
        if (Direction != Right)
        {
            Direction = Left;
            TurnsCounter++;
        }
    }

    private void DirRight()
    {
        if (Direction != Left)
        {
            Direction = Right;
            TurnsCounter++;
        }
            
        
    }
    //collision dectection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Food"))
        {
            ate = true;

            //get rid of the food 
            Destroy(collision.gameObject);
            //Timestep is used to increase the speed of the snake until a certain point 
            if (Timestep > 5)
            {
                Timestep--;
            }
            


        }
        //if the snake collides with anything else other then fodd its game over
        else
        {
           SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
    }
    //find position for the grid
   public Vector2 FindPosition(Vector2 Pos)
    {
        
        Gridx = (-3.5f - Pos.x) * -1;
        Gridx = (Gridx / 0.25f) - 1;
        Gridy = (3 - Pos.y);
        Gridy = (Gridy / 0.25f) - 1;
        

        Vector2 GridPos;
        GridPos.x = Gridx;
        GridPos.y = Gridy;
        return GridPos;
    }
    private void Reset()
    {
        StartTouch = Vector2.zero;
        SwipeDelta = Vector2.zero;
        isDragging = false;
    }
}


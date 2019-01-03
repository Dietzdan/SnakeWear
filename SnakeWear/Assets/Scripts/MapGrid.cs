using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    //Grid postions 25x21 of 0.25x0.25 sqaures
    int Gridx;
    int Gridy;
    //positon setting each Grid pos to the actualy game area pos 0.25x0.25 squares
    float PosX = -3.25f;
    float PosY = 2.75f;
    //25 and 21 so you can die 24x20 is actually playable area
    public GameObject SnakeHead;

    public Vector2[,] PlayArea = new Vector2[25, 21];

  

    // Use this for initialization
    void Awake()
    {
        //creating the grid
        for (Gridx = 0; Gridx < 25; Gridx++)
        {
            for (Gridy = 0; Gridy < 21; Gridy++)
            {
                PlayArea[Gridx, Gridy] = new Vector2(PosX, PosY);
                
                PosY -= 0.25f;
            }
            PosY = 2.75f;
            PosX += 0.25f;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
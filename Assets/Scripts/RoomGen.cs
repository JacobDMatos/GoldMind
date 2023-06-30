using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomGen : MonoBehaviour
{
    //Main map generation objects
    public static int floorSize = 5;
    public int CRRow = 0;
    public int CRCol = 0;
    public int[,] floor1 = new int[floorSize, floorSize];
    public bool[,] roomClear = new bool[floorSize, floorSize];
    public int floorSizeSet = 5;
    public GameObject player;
    //Minimap objects
    public GameObject miniRoom;
    public GameObject miniRoomClear;
    public GameObject miniBack;
    public float changeBy = 2;
    private float startMapPos = 8.82f;
    private float bigMapPos = 13.0f;
    //Room generation objects
    public int uniqueRooms1 = 9; // number of normal rooms
    public int uniqueRooms2 = 2; // number of boss rooms
    public GameObject rock;
    public int floorNum = 1;
    public float rockSpacing = 0.6f;
    public GameObject zombie;
    public GameObject skeleton;
    public GameObject spider;
    public GameObject boomer;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject water;
    public GameObject treasure;
    public GameObject coinObject;
    public int enemiesLeft = 0;
    public int sizedRoomChance = 4;
    public bool isBigRoom = false;
    public float cameraDistance = 6;

    public int shopItemCostMax = 18;
    public int shopItemCostMin = 12;
    public int shopConsumCostMax = 6;
    public int shopConsumCostMin = 3;

    

    // Start is called before the first frame update
    void Start()
    {
        floorSize = floorSizeSet;
        //KEY: 0 = not a room. 1 = normal room. 2 = boss room. 3 = starting room. 4 = treasure room. 5 = shop. 6 = 2x2 room. 7 = 2x1 room. 8 = 1x2 room. 9 = go down. 10 = go left. 11 go left down.
        int[,] floor = new int[floorSize, floorSize];
        bool[,] roomClear1 = new bool[floorSize, floorSize];
        bool finished = false;
        bool branching = false;
        int row = 0;
        int col = 0;
        int lastRow = 0, lastCol = 0;
        int rand = 0;
        int brRow = 0;
        int brCol = 0;

        floor[0, 0] = 3;
        
        while (!finished)
        {
            
            
            rand = Random.Range(0, 2); 
            if (rand == 0)
                row++;
            if (rand == 1)
                col++;

            //Branching
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                branching = true;
                brRow = row;
                brCol = col;
            }
            while (branching)
            {
                rand = Random.Range(0, 4);
                if (rand == 0)
                    brRow++;
                if (rand == 1)
                    brRow--;
                if (rand == 2)
                    brCol++;
                if (rand == 3)
                    brCol--;
                if (brCol >= floorSize || brCol < 0 || brRow >= floorSize || brRow < 0)
                {

                    branching = false;
                }
                else
                {
                    if (floor[brRow, brCol] == 0)
                    {
                        floor[brRow, brCol] = 1;
                        //Debug.Log("Branch: " + brRow + " " + brCol + "   " + floor[brRow, brCol]);
                    }
                    else
                        branching = false;
                }
            }


            if (row == floorSize || col == floorSize)
            {
                //Create boss room and finish
                floor[lastRow, lastCol] = 2;
                finished = true;
            }
            else
                floor[row, col] = 1;
            //Debug.Log("Default path: " + lastRow + " " + lastCol + "   " + floor[lastRow, lastCol]);
            lastRow = row;
            lastCol = col;
            floor1 = floor;
        }
        //treasure room route
        bool fallback = false;
        int doggo = Random.Range(0, 2);
        int treRow = Random.Range(0, floorSize - 1);
        int treCol = Random.Range(0, floorSize - 1);
        if (doggo == 0)
            treRow = floorSize - 2;
        else
            treCol = floorSize - 2;

        floor[treRow, treCol] = 4;
        //Debug.Log("treasure room at " + treRow + treCol);
        while (finished)
        {
            doggo = Random.Range(0, 2);
            //Debug.Log(doggo);
            if (doggo == 0 && treRow != 0)
            {
                treRow--;
                //Debug.Log("decreasing row");
            }
            else

                if (treCol != 0)
            {
                treCol--;
                //Debug.Log("decreasing col");
            }
            else
                fallback = true;
            if (floor[treRow, treCol] == 0)
            {
                floor[treRow, treCol] = 1;
                //Debug.Log("Spawning new room at" + treRow + treCol);
            }
            else if(!fallback)
            {
                //Debug.Log("stopped at " + treRow + treCol);
                finished = false;
            }
            fallback = false;
        }
        //end treasure room route
        //shop route
        fallback = false;
        doggo = Random.Range(0, 2);
        treRow = Random.Range(0, floorSize - 2);
        treCol = Random.Range(0, floorSize - 2);
        if (doggo == 0)
            treRow = floorSize - 3;
        else
            treCol = floorSize - 3;

        floor[treRow, treCol] = 5;
        //Debug.Log("treasure room at " + treRow + treCol);
        while (!finished)
        {
            doggo = Random.Range(0, 2);
            //Debug.Log(doggo);
            if (doggo == 0 && treRow != 0)
            {
                treRow--;
                //Debug.Log("decreasing row");
            }
            else

                if (treCol != 0)
            {
                treCol--;
                //Debug.Log("decreasing col");
            }
            else
                fallback = true;
            if (floor[treRow, treCol] == 0)
            {
                floor[treRow, treCol] = 1;
                //Debug.Log("Spawning new room at" + treRow + treCol);
            }
            else if (!fallback)
            {
                //Debug.Log("stopped at " + treRow + treCol);
                finished = true;
            }
            fallback = false;
        }
        //create big rooms
        for (int i = 0; i < floorSize; i++)
        {
            for (int j = 0; j < floorSize; j++)
            {
                if (floor[i,j] == 1)
                {
                    doggo = Random.Range(0, sizedRoomChance);
                    if (doggo == 0)
                    {
                        //make sure array doesnt go oob
                        if (i <= floorSize - 2 && j <= floorSize -2)
                        {
                            if (floor[i + 1, j] == 1 && floor[i, j + 1] == 1 && floor[i + 1, j + 1] == 1)
                            {
                                floor[i, j] = 6;
                                floor[i + 1, j] = 10;
                                floor[i, j + 1] = 9;
                                floor[i + 1, j + 1] = 11;
                            }
                            else if (floor[i + 1, j] == 1)
                            {
                                floor[i, j] = 7;
                                floor[i + 1, j] = 10;
                            }
                            else if (floor[i, j + 1] == 1)
                            {
                                floor[i, j] = 8;
                                floor[i , j + 1] = 9;
                            }
                        }
                    }
                }
            }
        }
                //Copy floor and roomClear1 into their public sets so that they can be used
        floor1 = floor;
        roomClear = roomClear1;
     
        DisplayMap();
        loadRoom(CRRow, CRCol);
    }

    // Update is called once per frame
    void Update()

    {
        
    }
    public void ChangeCol(int ch)
    {
        CRCol = CRCol + ch;
    }
    public int GetCol()
    {
        return CRCol;
    }
    public void ChangeRow(int ch)
    {
        CRRow = CRRow + ch;
    }
    public int GetRow()
    {
        return CRRow;
    }
    public float GetChangeBy()
    {
        return changeBy;
    }
    public int GetFloorSize()
    {
        return floorSizeSet;
    }
    public bool hasRoomLeft()
    {
        if (CRRow != 0)
            return true;
        return false;
    }
    public bool hasRoomRight()
    {
        if (CRRow < floorSizeSet)
            return true;
        return false;
    }
    public bool hasRoomDown()
    {
        if (CRCol != 0)
            return true;
        return false;
    }
    public bool hasRoomUp()
    {
        if (CRCol < floorSizeSet)
            return true;
        return false;
    }
    public int GetEnemiesLeft()
    {
        //probably nor necessary but a counter bug measure
        if (enemiesLeft < 0)
            enemiesLeft = 0;
        return enemiesLeft;
    }
    public void ChangeEnemiesLeft(int i)
    {
        enemiesLeft += i;
    }
    public int GetRoom(int r, int c)
    {
        if (r <= floorSize - 1 && c <= floorSize - 1 && r >= 0 && c >= 0)
        {
            return floor1[r, c];
        }
        return 0;
    }
    void DisplayMap()
    {
        float xpos = miniBack.transform.position.x;
        float ypos = miniBack.transform.position.y;
        float zpos = 0;
        for (int i = 0; i < floorSize; i++)
        {
            for (int j = 0; j < floorSize; j++)
            {
                if (GetRoom(i, j) != 0)
                {
                    GameObject miniBox = Instantiate(miniRoom, new Vector3((xpos + changeBy * (j* 1.0f + 0.5f - floorSize/2.0f)), (ypos + changeBy * (i*1.0f + 0.5f - floorSize / 2.0f)), zpos), transform.rotation) as GameObject;
                    miniBox.transform.parent = miniBack.transform;
                }
            }
        }
    }

    public void loadRoom(int r, int c)
    {
        if (floor1[r, c] <= 5 || floor1[r, c] >= 9)
        {
            isBigRoom = false;
        }
        else
        {
            isBigRoom = true;
        }
        if (isBigRoom)
        {
            miniBack.transform.position = new Vector3(bigMapPos, miniBack.transform.position.y, 0);
            gameObject.GetComponent<Camera>().orthographicSize = cameraDistance * 1.466f;
            if (floor1[r, c] == 6)
                transform.position = new Vector3(4f, 4f, -10);
            if (floor1[r, c] == 7)
                transform.position = new Vector3(0, 4f, -10);
            if (floor1[r, c] == 8)
                transform.position = new Vector3(4f, 0, -10);
            
        }
        else
        {
            gameObject.GetComponent<Camera>().orthographicSize = cameraDistance;
            transform.position = new Vector3(0, 0, -10);
            miniBack.transform.position = new Vector3(startMapPos, miniBack.transform.position.y, 0);
        }
        if (!roomClear[r, c])
        {
            //Debug.Log("loading room " + r + c);
            int failSafe = 0;
            //Debug.Log(floor1[r, c]);
            
            int layout = 1;
            if (floor1[r, c] == 1)
                layout = Random.Range(1, (uniqueRooms1 + 1));
            if (floor1[r, c] == 2)
                layout = Random.Range(1, (uniqueRooms2 + 1));

            string path = "Assets/Resources/FloorLayouts/Floor" + floorNum + "/" + floor1[r, c] + "Layout" + layout + ".txt";
            //TextAsset roomstxt = Resources.Load<TextAsset>("FloorLayouts/Floor" + floorNum + "/" + floor1[r, c] + "Layout" + layout);

            //string contents = roomstxt.ToString();

            using (StreamReader sr = File.OpenText(path))
            //using (StreamReader sr = roomstxt)
            {
                string s = "";
                //string s = sr.ReadLine();
                /*while (s != ("Floor:" + floorNum) && failSafe < 1000)
                {
                    failSafe++;
                    s = sr.ReadLine();
                }
                */
                /*
                while (s != ("Roomtype:" + floor1[r, c]) && failSafe < 1000)
                {
                    failSafe++;
                    s = sr.ReadLine();
                }
                */
                //code reads layout = Random.Range(1, total layouts - 1)
                /*
                int layout = 1;
                if (floor1[r,c] == 1)
                    layout = Random.Range(1, 5);
                if (floor1[r, c] == 3)
                    layout = Random.Range(1, 1);
                
                while (s != ("layout:") + layout && failSafe < 1000)
                {
                    failSafe++;
                    s = sr.ReadLine();
                }
                */
                while ((s = sr.ReadLine()) != null && failSafe < 1000)
                {
                    failSafe++;
                    //Debug.Log(s);
                    var sStrings = s.Split(","[0]);
                    //Debug.Log(sStrings[0]);
                    if (sStrings[0] == "rock")
                    {
                        GameObject newRock = Instantiate(rock, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                    }
                    if (sStrings[0] == "rockCluster")
                    {

                        GameObject newRock = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) - rockSpacing, float.Parse(sStrings[2]) + rockSpacing, 0), transform.rotation) as GameObject;
                        GameObject newRock2 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]) + rockSpacing, 0), transform.rotation) as GameObject;
                        GameObject newRock3 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) + rockSpacing, float.Parse(sStrings[2]) + rockSpacing, 0), transform.rotation) as GameObject;
                        GameObject newRock4 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) - rockSpacing, float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        GameObject newRock5 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) + rockSpacing, float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        GameObject newRock6 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) - rockSpacing, float.Parse(sStrings[2]) - rockSpacing, 0), transform.rotation) as GameObject;
                        GameObject newRock7 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]) - rockSpacing, 0), transform.rotation) as GameObject;
                        GameObject newRock8 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) + rockSpacing, float.Parse(sStrings[2]) - rockSpacing, 0), transform.rotation) as GameObject;

                    }
                    if (sStrings[0] == "rockLine")
                    {
                        GameObject newRock = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) - rockSpacing, float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        GameObject newRock2 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        GameObject newRock3 = Instantiate(rock, new Vector3(float.Parse(sStrings[1]) + rockSpacing, float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                    }
                    if (sStrings[0] == "zombie")
                    {
                        GameObject newThing = Instantiate(zombie, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        enemiesLeft++;
                    }
                    if (sStrings[0] == "skeleton")
                    {
                        GameObject newThing = Instantiate(skeleton, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        enemiesLeft++;
                    }
                    if (sStrings[0] == "spider")
                    {
                        GameObject newThing = Instantiate(spider, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        enemiesLeft++;
                    }
                    if (sStrings[0] == "treasure")
                    {
                        GameObject newThing = Instantiate(treasure, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                    }
                    if (sStrings[0] == "shopTreasure")
                    {
                        GameObject newThing = Instantiate(treasure, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        newThing.GetComponent<ItemIdScript>().cost = Random.Range(shopItemCostMin, shopItemCostMax);
                    }
                    if (sStrings[0] == "shopBomb")
                    {
                        GameObject newThing = Instantiate(treasure, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        newThing.GetComponent<ItemIdScript>().cost = Random.Range(shopConsumCostMin, shopConsumCostMax);
                        newThing.GetComponent<ItemIdScript>().isBomb = true;
                    }
                    if (sStrings[0] == "boomer")
                    {
                        GameObject newThing = Instantiate(boomer, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        enemiesLeft++;
                    }
                    if (sStrings[0] == "water")
                    {
                        GameObject newThing = Instantiate(water, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                    }
                    if (sStrings[0] == "boss1")
                    {
                        GameObject newThing = Instantiate(boss1, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        enemiesLeft++;
                    }
                    if (sStrings[0] == "boss2")
                    {
                        GameObject newThing = Instantiate(boss2, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                        enemiesLeft++;
                    }
                    if (sStrings[0] == "coin")
                    {
                        GameObject newThing = Instantiate(coinObject, new Vector3(float.Parse(sStrings[1]), float.Parse(sStrings[2]), 0), transform.rotation) as GameObject;
                    }
                        //Debug.Log(failSafe);
                }
                float xpos = miniBack.transform.position.x;
                float ypos = miniBack.transform.position.y;
                GameObject miniBox = Instantiate(miniRoomClear, new Vector3((xpos + changeBy * (c * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * (r * 1.0f + 0.5f - floorSize / 2.0f)), 0), transform.rotation) as GameObject;
                miniBox.transform.parent = miniBack.transform;
                if(floor1[r, c] == 6)
                {
                    GameObject miniBox2 = Instantiate(miniRoomClear, new Vector3((xpos + changeBy * ((c + 1) * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * (r * 1.0f + 0.5f - floorSize / 2.0f)), 0), transform.rotation) as GameObject;
                    miniBox2.transform.parent = miniBack.transform;
                    GameObject miniBox3 = Instantiate(miniRoomClear, new Vector3((xpos + changeBy * (c * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * ((r + 1) * 1.0f + 0.5f - floorSize / 2.0f)), 0), transform.rotation) as GameObject;
                    miniBox3.transform.parent = miniBack.transform;
                    GameObject miniBox4 = Instantiate(miniRoomClear, new Vector3((xpos + changeBy * ((c + 1) * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * ((r + 1) * 1.0f + 0.5f - floorSize / 2.0f)), 0), transform.rotation) as GameObject;
                    miniBox4.transform.parent = miniBack.transform;
                }
                if (floor1[r, c] == 7)
                {
                    GameObject miniBox2 = Instantiate(miniRoomClear, new Vector3((xpos + changeBy * (c * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * ((r + 1) * 1.0f + 0.5f - floorSize / 2.0f)), 0), transform.rotation) as GameObject;
                    miniBox2.transform.parent = miniBack.transform;
                }
                if (floor1[r, c] == 8)
                {
                    GameObject miniBox2 = Instantiate(miniRoomClear, new Vector3((xpos + changeBy * ((c + 1) * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * (r * 1.0f + 0.5f - floorSize / 2.0f)), 0), transform.rotation) as GameObject;
                    miniBox2.transform.parent = miniBack.transform;
                }
                roomClear[r, c] = true;
                //sr next line
                return;
            }
        }
     }
}

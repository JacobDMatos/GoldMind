using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public int direction = 0;
    public new GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        int r = camera.GetComponent<RoomGen>().GetRow();
        int c = camera.GetComponent<RoomGen>().GetCol();

            //Debug.Log(direction + "  " + camera.GetComponent<RoomGen>().GetRoom(r, c));
            
        

            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (direction == 4 || direction == 5 || direction == 6)
            {
                if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 7)
                {
                    gameObject.GetComponent<Renderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }
            }
            if (direction == 4 || direction == 5 || direction == 8 || direction == 7 || direction == 10 || direction == 11)
            {
                if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 6)
                {
                    gameObject.GetComponent<Renderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }
            }
            if (direction == 9 || direction == 10 || direction == 11)
            {
                if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 8)
                {
                    gameObject.GetComponent<Renderer>().enabled = true;
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }
            }
        if (direction == 0 && camera.GetComponent<RoomGen>().GetRoom(r, c) != 7 && camera.GetComponent<RoomGen>().GetRoom(r, c) != 6)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        if (direction == 1 && camera.GetComponent<RoomGen>().GetRoom(r, c) != 8 && camera.GetComponent<RoomGen>().GetRoom(r, c) != 6)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

    }
}

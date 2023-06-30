using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDoor : MonoBehaviour
    
{
    //key 0 = up, 1 = right, 2 = down, 3 = left, 4 = up left, 5 = Up 2, 6 = Up right (1), 7 = up right 2, 8 = up 2 right, 9 = up right (2), 10 = right 2, 11 = down right
    public int direction = 0;
    public new GameObject camera;
    public float roomDif = 3.2f;
    private float roomChange = 4.1f;

    // Start is called before the first frame update
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Entering door");
            if (direction == 1)
            {
                camera.GetComponent<RoomGen>().ChangeCol(1);
                other.gameObject.transform.position = new Vector3(-roomDif, 0);
                //Debug.Log(camera.GetComponent<RoomGen>().GetCol());
            }
            if (direction == 3)
            {
                camera.GetComponent<RoomGen>().ChangeCol(-1);
                other.gameObject.transform.position = new Vector3(roomDif, 0);
                //Debug.Log(camera.GetComponent<RoomGen>().GetCol());
            }
            if (direction == 0)
            {
                camera.GetComponent<RoomGen>().ChangeRow(1);
                other.gameObject.transform.position = new Vector3(0, -roomDif);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 2)
            {
                camera.GetComponent<RoomGen>().ChangeRow(-1);
                other.gameObject.transform.position = new Vector3(0, roomDif);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 4)
            {
                camera.GetComponent<RoomGen>().ChangeRow(1);
                camera.GetComponent<RoomGen>().ChangeCol(-1);
                other.gameObject.transform.position = new Vector3(roomDif, 0);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 5)
            {
                camera.GetComponent<RoomGen>().ChangeRow(2);
                other.gameObject.transform.position = new Vector3(0, -roomDif);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 6)
            {
                camera.GetComponent<RoomGen>().ChangeRow(1);
                camera.GetComponent<RoomGen>().ChangeCol(1);
            other.gameObject.transform.position = new Vector3(-roomDif, 0);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 7)
            {
                camera.GetComponent<RoomGen>().ChangeRow(1);
                camera.GetComponent<RoomGen>().ChangeCol(2);
                other.gameObject.transform.position = new Vector3(-roomDif, 0);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 8)
            {
                camera.GetComponent<RoomGen>().ChangeRow(2);
                camera.GetComponent<RoomGen>().ChangeCol(1);
                other.gameObject.transform.position = new Vector3(0, -roomDif);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 9)
            {
                camera.GetComponent<RoomGen>().ChangeRow(1);
                camera.GetComponent<RoomGen>().ChangeCol(1);
                other.gameObject.transform.position = new Vector3(0, -roomDif);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 10)
            {
                camera.GetComponent<RoomGen>().ChangeCol(2);
                other.gameObject.transform.position = new Vector3(-roomDif, 0);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            if (direction == 11)
            {
                camera.GetComponent<RoomGen>().ChangeRow(-1);
                camera.GetComponent<RoomGen>().ChangeCol(1);
                other.gameObject.transform.position = new Vector3(0, roomDif);
                //Debug.Log(camera.GetComponent<RoomGen>().GetRow());
            }
            int r = camera.GetComponent<RoomGen>().GetRow();
            int c = camera.GetComponent<RoomGen>().GetCol();
            //KEY for roomgen: 0 = not a room. 1 = normal room. 2 = boss room. 3 = starting room. 4 = treasure room. 5 = shop. 6 = 2x2 room. 7 = 2x1 room. 8 = 1x2 room. 9 = go down. 10 = go left. 11 go left down.
            //key for direction: 0 = up, 1 = right, 2 = down, 3 = left, 4 = up left, 5 = Up 2, 6 = Up right (1), 7 = up right 2, 8 = up 2 right, 9 = up right (2), 10 = right 2, 11 = down right
            //Debug.Log(camera.GetComponent<RoomGen>().GetRoom(r, c));
            if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 9)
            {
                camera.GetComponent<RoomGen>().ChangeCol(-1);
                c--;
                other.gameObject.transform.position = new Vector3(roomChange * 2, 0) + other.gameObject.transform.position;
            }
            if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 10)
            {
                camera.GetComponent<RoomGen>().ChangeRow(-1);
                r--;
                other.gameObject.transform.position = new Vector3(0, roomChange * 2) + other.gameObject.transform.position;
            }
            if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 11)
            {
                camera.GetComponent<RoomGen>().ChangeCol(-1);
                camera.GetComponent<RoomGen>().ChangeRow(-1);
                r--;
                c--;
                other.gameObject.transform.position = new Vector3(roomChange * 2, roomChange * 2) + other.gameObject.transform.position;
            }
            
            camera.GetComponent<RoomGen>().loadRoom(r, c);

        }
    }
    void Update()
    {
        int enemiesLeft = camera.GetComponent<RoomGen>().GetEnemiesLeft();
        int r = camera.GetComponent<RoomGen>().GetRow();
        int c = camera.GetComponent<RoomGen>().GetCol();
        if (direction == 0)
            r = r + 1;
        if (direction == 2)
            r = r - 1;
        if (direction == 1)
            c = c + 1;
        if (direction == 3)
            c = c - 1;
        if (direction <= 3)
        {
            if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 0 || enemiesLeft > 0)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
            //Debug.Log(direction + "  " + camera.GetComponent<RoomGen>().GetRoom(r, c));
            if (direction == 0 && camera.GetComponent<RoomGen>().GetRoom(r, c) == 10)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            if (direction == 1 && camera.GetComponent<RoomGen>().GetRoom(r, c) == 9)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
        r = camera.GetComponent<RoomGen>().GetRow();
        c = camera.GetComponent<RoomGen>().GetCol();
        if (direction >= 4)
        {
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
            switch (direction)
            {
                case 0:
                    r++;
                    break;
                case 1:
                    c++;
                    break;
                case 2:
                    r--;
                    break;
                case 3:
                    c--;
                    break;
                case 4:
                    c--;
                    r++;
                    break;
                case 5:
                    r+=2;
                    break;
                case 6:
                    c++;
                    r++;
                    break;
                case 7:
                    c+=2;
                    r++;
                    break;
                case 8:
                    c++;
                    r+=2;
                    break;
                case 9:
                    c++;
                    r++;
                    break;
                case 10:
                    c+=2;
                    break;
                case 11:
                    c++;
                    r--;
                    break;
                default:
                    break;
            }
            if (camera.GetComponent<RoomGen>().GetRoom(r, c) == 0 || enemiesLeft > 0)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    //key 0 = up, 1 = right, 2 = down, 3 = left, 4 = up left, 5 = Up 2, 6 = Up right (1), 7 = up right 2, 8 = up 2 right, 9 = up right (2), 10 = right 2, 11 = down right
}

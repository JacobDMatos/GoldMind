using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public int spawnRow = 0;
    public int spawnCol = 0;
    private new GameObject camera;
    public bool isItActive = true;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        spawnRow = camera.GetComponent<RoomGen>().GetRow();
        spawnCol = camera.GetComponent<RoomGen>().GetCol();
    }

    // Update is called once per frame
    void Update()
    {
        int r = camera.GetComponent<RoomGen>().GetRow();
        int c = camera.GetComponent<RoomGen>().GetCol();
        if (r == spawnRow && c == spawnCol)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            isItActive = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            isItActive = false;
        }
    }
}

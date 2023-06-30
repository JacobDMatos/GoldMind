using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    public GameObject miniBack;
    public float changeBy = 2;
    public new GameObject camera;
    public int floorSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        changeBy = camera.GetComponent<RoomGen>().GetChangeBy();
        floorSize = camera.GetComponent<RoomGen>().GetFloorSize();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRoom();
    }
    void MoveRoom()
    {
        float xpos = miniBack.transform.position.x;
        float ypos = miniBack.transform.position.y;
        float zpos = 0;
        int roomRow = camera.GetComponent<RoomGen>().GetRow();
        int roomCol = camera.GetComponent<RoomGen>().GetCol();
        transform.position = new Vector3((xpos + changeBy * (roomCol * 1.0f + 0.5f - floorSize / 2.0f)), (ypos + changeBy * (roomRow * 1.0f + 0.5f - floorSize / 2.0f)), zpos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkeleShot : MonoBehaviour
{
    public float rotationspeed;
    //private float currentRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //currentRot -= rotationspeed;
        transform.Rotate(new Vector3(0, 0, rotationspeed * Time.deltaTime));
    }
}

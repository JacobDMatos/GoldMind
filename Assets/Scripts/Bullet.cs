using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        Onward();
    }
    public void Onward()
    {
        //transform.position = transform.position + transform.forward * speed;
        /// 57.2958 looks weird but thats the constant for degrees to radians to dont change it
        transform.position = transform.position + transform.right * speed * Mathf.Cos(transform.rotation.z / 57.2958f);
        transform.position = transform.position + transform.up * speed * Mathf.Sin(transform.rotation.z / 57.2958f);
    }
}

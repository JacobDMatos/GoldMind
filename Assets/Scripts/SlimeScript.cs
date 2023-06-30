using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float moveSpeed;

    private Transform other;
    private Rigidbody2D rb;

    void Start()
    {
        other = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            float xdiff = transform.position.x - other.transform.position.x;
            //Debug.Log(xdiff);
            float ydiff = transform.position.y - other.transform.position.y;

            float shotRotation = 90;

            if (xdiff != 0)
            {
                shotRotation = Mathf.Atan(ydiff / xdiff) * 57.2958f;
                if (xdiff > 0)
                    shotRotation += 180;
            }
            else
            {
                //Debug.Log(ydiff);
                if (ydiff > 0)
                {

                    shotRotation = 270;
                }
            }
            shotRotation = shotRotation / 57.2958f;
            rb.AddForce(transform.up * moveSpeed * Mathf.Sin(shotRotation));
            rb.AddForce(transform.right * moveSpeed * Mathf.Cos(shotRotation));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerScript : MonoBehaviour
{
    public GameObject bomb;

    private Transform player;
    private float timer;
    public float maxTime = 2.0f;

    public float shootSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            float xdiff = transform.position.x - player.position.x;
            //Debug.Log(xdiff);
            float ydiff = transform.position.y - player.position.y;

            float diffTotal = Mathf.Abs(xdiff) + Mathf.Abs(ydiff);
            xdiff = xdiff / diffTotal;
            ydiff = ydiff / diffTotal;
            //Debug.Log("Rotating " + shotRotation);
            GameObject newBomb = Instantiate(bomb, transform.position, Quaternion.identity) as GameObject;

            newBomb.GetComponent<Rigidbody2D>().AddForce(newBomb.transform.right * shootSpeed * -xdiff);
            newBomb.GetComponent<Rigidbody2D>().AddForce(newBomb.transform.up * shootSpeed * -ydiff);
            timer = 0;
        }
    }
}

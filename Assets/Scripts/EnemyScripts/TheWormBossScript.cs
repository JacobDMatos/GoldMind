using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWormBossScript : MonoBehaviour
{
    public float timer;
    public float shotDelayTime;
    public GameObject shot;
    private Transform player;
    public float shotGap = 7.5f;
    public Rigidbody2D rb;
    public float speed;
    public float impactDelay = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        move();
        impactDelay += Time.deltaTime;
    }
    void shoot()
    {
        timer += Time.deltaTime;
        if (timer >= shotDelayTime)
        {
            int randy = Random.Range(2, 6);
            float xdiff = transform.position.x - player.position.x;
            //Debug.Log(xdiff);
            float ydiff = transform.position.y - player.position.y;

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

            //Debug.Log("Rotating " + shotRotation);
            for (float i = 0; i < randy; i++) {
                GameObject newEnemyShot = Instantiate(shot, transform.position, Quaternion.identity) as GameObject;
                float mathDiff = (i - ((randy - 1) / 2.0f)) * shotGap;
                //Debug.Log(mathDiff);
                newEnemyShot.transform.Rotate(new Vector3(0, 0, shotRotation + mathDiff));
            }
            timer = 0;
        }
    }
    void move()
    {
        transform.position = transform.position + transform.right * speed * Mathf.Cos(transform.rotation.z / 57.2958f) * Time.deltaTime;
        transform.position = transform.position + transform.up * speed * Mathf.Sin(transform.rotation.z / 57.2958f) * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (impactDelay >= 0.4f && col.collider.CompareTag("Obstacle"))
        {
            transform.Rotate(new Vector3(0, 0, -90));
            impactDelay = 0;
        }
    }
}

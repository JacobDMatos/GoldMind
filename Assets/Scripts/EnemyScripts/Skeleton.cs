using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject shot;
    public float shotDelay;

    private Transform player, shotSpawn;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shotSpawn = transform.GetChild(0);
        timer = shotDelay;
    }

    void Update()
    {
        Aim();

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            
            float xdiff = shotSpawn.position.x - player.position.x;
            //Debug.Log(xdiff);
            float ydiff = shotSpawn.position.y - player.position.y;
            
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
            GameObject newEnemyShot = Instantiate(shot, shotSpawn.position, Quaternion.identity) as GameObject;
            newEnemyShot.transform.Rotate(new Vector3(0, 0, shotRotation));
            timer = shotDelay;
        }
    }

    void Aim()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

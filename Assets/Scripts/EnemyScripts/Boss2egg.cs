using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2egg : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public GameObject egg;
    public float maxTimer;
    public float max2Timer;
    private float Stimer;
    private float corner = 3.3f;
    private bool didShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Stimer < maxTimer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            
        } else if (!didShoot)
        {
            spawnEgg();
        }
        if (Stimer < max2Timer + maxTimer)
        {
            Stimer += Time.deltaTime;
        }
        else
        {
            Stimer = 0;
            didShoot = false;
        }
    }
    void spawnEgg()
    {
        if (player.transform.position.y < 0)
        {
            GameObject newEnemyShot = Instantiate(egg, new Vector3(corner, corner, 0), Quaternion.identity) as GameObject;
            GameObject newEnemyShot2 = Instantiate(egg, new Vector3(-corner, corner, 0), Quaternion.identity) as GameObject;
        }
        else
        {
            GameObject newEnemyShot = Instantiate(egg, new Vector3(corner, -corner, 0), Quaternion.identity) as GameObject;
            GameObject newEnemyShot2 = Instantiate(egg, new Vector3(-corner, -corner, 0), Quaternion.identity) as GameObject;
        }
        didShoot = true;
    }
}

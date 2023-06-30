using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private GameObject p1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerShot") || other.gameObject.CompareTag("EnemyShot"))
        {
            if (other.gameObject.CompareTag("PlayerShot"))
            {
                p1 = GameObject.FindGameObjectWithTag("Player");
                if (p1.GetComponent<PlayerThings>().bombBullets)
                {
                    GameObject explosion = Instantiate(Resources.Load("Explosion"), other.transform.position, Quaternion.identity) as GameObject;
                }
            }
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float health;

    private GameObject player;
    private float playerDamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerDamage = player.GetComponent<PlayerThings>().GetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            health -= playerDamage;
            Destroy(other.gameObject);
        }
    }
}

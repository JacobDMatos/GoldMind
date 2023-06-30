using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestruction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explosion"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    float timer = 0;
    public float delay = 0.25f;
    public float moveBy;
    public float explosionTimer = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, explosionTimer);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            transform.position = new Vector3(transform.position.x + Random.Range(-moveBy, moveBy), transform.position.y + Random.Range(-moveBy, moveBy), 0);
            timer = 0;
        }
    }
}

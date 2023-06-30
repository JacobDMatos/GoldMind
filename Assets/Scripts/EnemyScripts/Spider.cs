using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float moveDelay;

    private Rigidbody2D spiderBody;
    private float timer;

    void Start()
    {
        spiderBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            spiderBody.MovePosition(transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0.0f));
            timer = moveDelay;
        }
    }
}

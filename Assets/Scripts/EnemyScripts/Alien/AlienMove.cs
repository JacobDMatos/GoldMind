using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMove : MonoBehaviour
{
    private ContactFilter2D filter;
    private int hitCount;
    private Collider2D[] hits;
    private bool isTeleporting;

    void Start()
    {
        filter.SetLayerMask(LayerMask.GetMask("Player"));
        hits = new Collider2D[10];
        hitCount = 0;
        isTeleporting = false;
    }

    void Update()
    {
        hitCount = Physics2D.OverlapCircle(transform.position, 1.0f, filter, hits);

        if (hitCount > 0)
        {
            if (!isTeleporting)
            {
                StartCoroutine("Teleport");
            }
        }
    }

    IEnumerator Teleport()
    {
        isTeleporting = true;

        Vector2 newPosition = new Vector2(UnityEngine.Random.Range(-3.0f, 3.0f), UnityEngine.Random.Range(-3.0f, 3.0f));

        while (Physics2D.OverlapCircle(newPosition, 1.0f, 0, 0.0f, 0.0f) != null)
        {
            newPosition = new Vector2(UnityEngine.Random.Range(-3.0f, 3.0f), UnityEngine.Random.Range(-3.0f, 3.0f));
        }

        yield return new WaitForSeconds(1.0f);

        transform.position = newPosition;

        isTeleporting = false;
    }
}

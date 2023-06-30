using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    private SpriteRenderer circleIndicator;

    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        circleIndicator = GetComponent<SpriteRenderer>();
        circleIndicator.color = new Color(0, 1, 0, 0.5f);
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.0f);

        circleIndicator.color = new Color(1, 1, 0, 0.5f);

        yield return new WaitForSeconds(1.0f);

        GetComponent<CircleCollider2D>().enabled = true;
        circleIndicator.color = new Color(1, 0, 0, 0.5f);

        yield return new WaitForSeconds(3.0f);

        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberDog : MonoBehaviour
{
    public float moveSpeed;

    private Transform player, AOE;
    private ContactFilter2D filter;
    private int hitCount;
    private Collider2D[] hits;
    private bool isAttacking;
    private SpriteRenderer circleIndicator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AOE = transform.GetChild(0);
        AOE.GetComponent<CircleCollider2D>().enabled = false;
        circleIndicator = AOE.GetChild(0).GetComponent<SpriteRenderer>();
        circleIndicator.color = new Color(0, 1, 0, 0.5f);
        filter.SetLayerMask(LayerMask.GetMask("Player"));
        hits = new Collider2D[10];
        hitCount = 0;
        isAttacking = false;
    }

    void Update()
    {
        hitCount = Physics2D.OverlapCircle(transform.position, 1.25f, filter, hits);

        if (hitCount > 0)
        {
            if (!isAttacking)
            {
                StartCoroutine("Attack");
            }
        } else
        {
            if (!isAttacking)
            {
                Move();
            }
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        circleIndicator.color = new Color(1, 1, 0, 0.5f);

        yield return new WaitForSeconds(1.5f);

        AOE.GetComponent<CircleCollider2D>().enabled = true;
        circleIndicator.color = new Color(1, 0, 0, 0.5f);

        yield return new WaitForSeconds(5.0f);

        AOE.GetComponent<CircleCollider2D>().enabled = false;
        circleIndicator.color = new Color(0, 1, 0, 0.5f);

        isAttacking = false;
    }
}

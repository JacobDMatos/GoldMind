using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject targetCircle, battery, enemy;
    
    private Transform leftSpawn, rightSpawn;
    private int batteryCount, circleCount;
    private bool isAttacking, isVulnerable;

    void Start()
    {
        RespawnBatteries();
        isAttacking = false;
        isVulnerable = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        leftSpawn = gameObject.transform.GetChild(0).transform;
        rightSpawn = gameObject.transform.GetChild(1).transform;
    }

    void Update()
    {
        batteryCount = GameObject.FindGameObjectsWithTag("Battery").Length;

        switch (batteryCount)
        {
            case 1:
                circleCount = 4;
                break;
            case 2:
                circleCount = 3;
                break;
            case 3:
                circleCount = 2;
                break;
            case 4:
                circleCount = 1;
                break;
        }

        if (batteryCount > 0)
        {
            if (!isAttacking)
            {
                StartCoroutine("Attack");
            }
        } else
        {
            if (!isVulnerable)
            {
                StartCoroutine("Vulnerable");
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        for (int i = 0; i < circleCount; i++)
        {
            Instantiate(targetCircle, new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), UnityEngine.Random.Range(-3.0f, 3.0f), 0.0f), Quaternion.identity);
        }

        yield return new WaitForSeconds(5.0f);

        isAttacking = false;
    }

    IEnumerator Vulnerable()
    {
        isVulnerable = true;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(2.0f);

        Instantiate(enemy, leftSpawn.position, Quaternion.identity);
        Instantiate(enemy, rightSpawn.position, Quaternion.identity);

        yield return new WaitForSeconds(8.0f);

        RespawnBatteries();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        isVulnerable = false;
    }

    void RespawnBatteries()
    {
        Instantiate(battery, new Vector3(-3.0f, -3.0f, 0.0f), Quaternion.identity);
        Instantiate(battery, new Vector3(-3.0f, 3.0f, 0.0f), Quaternion.identity);
        Instantiate(battery, new Vector3(3.0f, 3.0f, 0.0f), Quaternion.identity);
        Instantiate(battery, new Vector3(3.0f, -3.0f, 0.0f), Quaternion.identity);
    }
}

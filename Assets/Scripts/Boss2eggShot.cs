using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2eggShot : MonoBehaviour
{
    public float fadeSpeed = 0.05f;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rotateAtPlayer();
        StartCoroutine("ZoomIn");
    }

    // Update is called once per frame
    IEnumerator ZoomIn()
    {
        for (float f = 0.05f; f <= 1; f += fadeSpeed)
        {
            transform.localScale = new Vector3(f, f, 1);
            yield return new WaitForSeconds(fadeSpeed);
        }
        //gameObject.GetComponent<Collider2D>().enabled = true;
    }
    void rotateAtPlayer()
    {
        float xdiff = transform.position.x - player.position.x;
        //Debug.Log(xdiff);
        float ydiff = transform.position.y - player.position.y;

        float shotRotation = 90;

        if (xdiff != 0)
        {
            shotRotation = Mathf.Atan(ydiff / xdiff) * 57.2958f;
            if (xdiff > 0)
                shotRotation += 180;
        }
        else
        {
            //Debug.Log(ydiff);
            if (ydiff > 0)
            {

                shotRotation = 270;
            }
        }
        transform.Rotate(new Vector3(0, 0, shotRotation));
    }
}

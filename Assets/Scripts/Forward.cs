using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float lastEnd = 5.0f;
    public GameObject platform;
    public float xdif;
    // Start is called before the first frame update
    void Start()
    {
        xdif = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        player.transform.position = new Vector3(player.transform.position.x + xInput*speed*Time.deltaTime, player.transform.position.y, player.transform.position.z +speed * Time.deltaTime);
        if (lastEnd <= player.transform.position.z)
        {
            xdif += Random.Range(-5.0f, 5.0f);
            GameObject Dees = Instantiate(platform, new Vector3(0 + xdif, 0, lastEnd + 5.0f), transform.rotation) as GameObject;
            lastEnd = lastEnd + 10;
        }
    }
}

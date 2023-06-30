using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotAudio : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    // Start is called before the first frame update
    void Start()
    {
        int randy = Random.Range(0, 3);
        if (randy == 0)
        {
            audio1.Play();
        }
        if (randy == 1)
        {
            audio2.Play();
        }
        if (randy == 2)
        {
            audio3.Play();
        }
    }

    // Update is called once per frame

}

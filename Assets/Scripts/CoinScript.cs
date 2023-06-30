using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int value = 1;
    // Start is called before the first frame update
    void Start()
    {
        int randy = Random.Range(0, 10) + 1;
        if (randy <= 9)
            value = 1;
        if (randy == 10)
            value = 5;

    }

    // Update is called once per frame
}

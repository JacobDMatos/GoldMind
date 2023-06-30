using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<RockScript>().isItActive)
            gameObject.GetComponent<TextMeshPro>().enabled = true;
        else
            gameObject.GetComponent<TextMeshPro>().enabled = false;
    }
}

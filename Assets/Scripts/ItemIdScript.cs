using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemIdScript : MonoBehaviour
{
    //The total number of unique items that should be in the pool
    private int totalItems = 4;
    //The total number of unique stat upgrades possible
    private int totalStats = 3;
    //This item's unique item id
    public int itemId = 1;
    //Bool to decide if item should increase stats or if its an item
    public bool isStat;
    public int cost = 0;
    TextMeshPro TextMeshProObject;
    public bool isBomb = false;

    // Start is called before the first frame update
    void Start()
    {
        isStat = false;
        int randy = 0;
        randy = Random.Range(0, 2);
        //<= used instead of == in case the probability should be changed later
        if (randy <= 0)
            isStat = true;
        if (isStat)
            itemId = Random.Range(0, totalStats) + 1;
        if (!isStat)
            itemId = Random.Range(0, totalItems) + 1;
        if (cost > 0)
        {
            TextMeshProObject = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
            TextMeshProObject.text = "$ " + cost;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        //Debug.Log(isStat);
        //Debug.Log(itemId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

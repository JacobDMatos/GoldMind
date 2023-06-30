using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float fuseTimer = 2.0f;
    public GameObject mainMap;
    public GameObject explosion;
    public int CRRow;
    public int CRCol;
    // Start is called before the first frame update
    void Start()
    {
        mainMap = GameObject.FindGameObjectWithTag("MainCamera");
        CRCol = mainMap.GetComponent<RoomGen>().CRCol;
        CRRow = mainMap.GetComponent<RoomGen>().CRRow;
        gameObject.GetComponent<RockScript>().spawnRow = CRRow;
        gameObject.GetComponent<RockScript>().spawnCol = CRCol;
        StartCoroutine("blowUp");
        
    }
    IEnumerator blowUp()
    {
        yield return new WaitForSeconds(fuseTimer / 4.0f);
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        yield return new WaitForSeconds(fuseTimer / 2.0f + fuseTimer / 4.0f);
        GameObject newBoom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        newBoom.GetComponent<RockScript>().spawnRow = CRRow;
        newBoom.GetComponent<RockScript>().spawnCol = CRCol;
        Destroy(gameObject);
    }
    // Update is called once per frame
}

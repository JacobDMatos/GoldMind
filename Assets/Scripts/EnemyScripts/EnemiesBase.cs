using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBase : MonoBehaviour
{
    public float health = 50;
    public float damage = 3;
    float playerDamage;
    GameObject player;
    new GameObject camera;
    public bool isBoss;
    private GameObject healthBar;
    private float defaultHealthbarSizex = 10.0f;
    private float defaultHealthbarSizey = 1f;
    private Vector3 healthBarStartPos;
    private float startingHealth;
    public float percentChanceToDropCoin = 20;
    private float checkCoin;
    public float knockBackPercent = 0;
    public float linearDrag = 1.1f;
    private Rigidbody2D rb;
    public bool spawnsBabies= false;
    public GameObject babies;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        playerDamage = player.GetComponent<PlayerThings>().GetDamage();
        rb = gameObject.GetComponent<Rigidbody2D>();
        checkCoin = Random.value;
        rb.drag = linearDrag;
        healthBarStartPos = new Vector3(0, -5, 0);
        if (isBoss)
        {
            startingHealth = health;
            GameObject newHealthBar = Instantiate(Resources.Load("Healthbar"), healthBarStartPos, Quaternion.identity) as GameObject;
            healthBar = newHealthBar;

                healthBar.transform.position = new Vector3(healthBarStartPos.x - ((startingHealth - health) / startingHealth * defaultHealthbarSizex), healthBarStartPos.y, 0);
                healthBar.transform.localScale = new Vector3(defaultHealthbarSizex - ((startingHealth - health) / startingHealth * defaultHealthbarSizex), defaultHealthbarSizey, 1.0f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(transform.up * player.GetComponent<PlayerThings>().GetKnockback());
        if (health <= 0)
        {
            if (isBoss)
                Destroy(healthBar);
            if (spawnsBabies)
            {
                //Debug.Log("spawning babies");
                GameObject baby1 = Instantiate(babies, transform.position - new Vector3(0.3f,0,0), Quaternion.identity) as GameObject;
                baby1.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                baby1.GetComponent<EnemiesBase>().spawnsBabies = false;
                baby1.GetComponent<EnemiesBase>().health = 20;
                GameObject baby2 = Instantiate(babies, transform.position + new Vector3(0.3f, 0, 0), Quaternion.identity) as GameObject;
                baby2.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                baby2.GetComponent<EnemiesBase>().spawnsBabies = false;
                baby2.GetComponent<EnemiesBase>().health = 20;
            }
            //Debug.Log(checkCoin + " " + percentChanceToDropCoin);
            if (checkCoin < percentChanceToDropCoin / 100.0)
            {
                GameObject newcoin = Instantiate(Resources.Load("Coin"), transform.position, Quaternion.identity) as GameObject;
                //Debug.Log("SPAWNING COIN");
            }
            //Debug.Log("not here right?");
            Destroy(gameObject);
            camera.GetComponent<RoomGen>().ChangeEnemiesLeft(-1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("contact");
        if (other.CompareTag("PlayerShot"))
        {
            
            health -= playerDamage;
            //Destroy should be replace by change shot to a destorying shot animation later and turn off its collider
            if (0 < player.GetComponent<PlayerThings>().GetKnockback())
            {
                float xdiff = transform.position.x - other.transform.position.x;
                //Debug.Log(xdiff);
                float ydiff = transform.position.y - other.transform.position.y;

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
                shotRotation = shotRotation / 57.2958f;
                rb.AddForce(transform.up * player.GetComponent<PlayerThings>().GetKnockback() * knockBackPercent * -Mathf.Sin(shotRotation));
                rb.AddForce(transform.right * player.GetComponent<PlayerThings>().GetKnockback() * knockBackPercent * -Mathf.Cos(shotRotation));
                //I love math
                //Debug.Log("Up force: " + -Mathf.Cos(shotRotation));
                //Debug.Log("Right force: " + -Mathf.Sin(shotRotation));
            }
            Destroy(other.gameObject);
        }
        //health--
        if (other.CompareTag("Explosion"))
        {
            health -= playerDamage * 5;
        }
        if (isBoss)
        {
            healthBar.transform.position = new Vector3(healthBarStartPos.x - ((startingHealth - health) / startingHealth * defaultHealthbarSizex), healthBarStartPos.y, 0);
            healthBar.transform.localScale = new Vector3(defaultHealthbarSizex - ((startingHealth - health) / startingHealth * defaultHealthbarSizex), defaultHealthbarSizey, 1.0f);
        }
        
    }
}

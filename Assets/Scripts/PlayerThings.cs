using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerThings : MonoBehaviour
{
    public GameObject playerShot;
    public GameObject bomb;
    public GameObject healthBar;

    public Vector3 healthBarStartPos;
    public float defaultHealthbarSizex = 3.0f;
    public float defaultHealthbarSizey = 0.5f;
    public int sceneToLoadOnDeath = 0;

    public float speed;
    public float dhMult = 1.6f;
    public float health;
    public float maxHealth;
    public float knockBack = 1.2f;

    public float shotCooldownTimer;
    public float shotSpeed = 4;
    public float shotCooldown;
    public float shotInnacuracy = 0;
    public float shotRange = 2;
    public float damage = 10;
    public float deathTimer = 0;
    public int bombsLeft;
    public int coins;

    public TextMeshPro coinsText;
    public TextMeshPro bombsText;

    private float xInput, yInput;
    private bool isMoving, dhReady, deadHard;
    private Rigidbody2D playerBody;

    public GameObject healthbarMask;
    public Vector3 maskStart;

    //Item bools. Change to true when you get an item. Private values only so the inspector wont be gross. 
    public bool hasDualWield, canFly, bombBullets;

   

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        playerBody = GetComponent<Rigidbody2D>();
        deadHard = false;
        dhReady = true;
        shotCooldownTimer = 0;
        healthBarStartPos = healthBar.transform.position;
        maskStart = healthbarMask.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine("UseDeadHard");
        Move();

        
        if(!deadHard)
        {
            placeBomb();
            Shoot();
        }
        updateTextElements();
        //updateHealthBar();
        updateHealthBarMask();
        checkIsDead();
        WinCondition();
    }
    public void placeBomb()
    {
        if (Input.GetKeyDown("e"))
        {
            if (bombsLeft > 0)
            {
                GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
                bombsLeft--;
            }
        }
    }
    IEnumerator UseDeadHard()
    {

        if (Input.GetKey(KeyCode.LeftShift) && dhReady)
        {
            deadHard = true;
            dhReady = false;
            speed = speed * dhMult;

            yield return new WaitForSeconds(0.25f);

            speed = 5;
            deadHard = false;

            yield return new WaitForSeconds(2.0f);

            dhReady = true;
        }
    }

    public void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        isMoving = (xInput != 0 || yInput != 0);

        if (isMoving)
        {
            var moveVector = new Vector3(xInput, yInput, 0);
            playerBody.MovePosition(new Vector2((transform.position.x + moveVector.x * speed * Time.deltaTime), (transform.position.y + moveVector.y * speed * Time.deltaTime)));
        }
    }
   
    public void Shoot()
    {
        if (shotCooldownTimer <= 0)
        {

            if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
            {
                SpawnShot();
                if (hasDualWield)
                    SpawnShot();
            }
        }
        else
        {
            shotCooldownTimer--;
            if (shotCooldownTimer < 0)
                shotCooldownTimer = 0;
        }
    }
    public void SpawnShot()
    {
        GameObject Dees = Instantiate(playerShot, transform.position, transform.rotation) as GameObject;

        Dees.GetComponent<Bullet>().speed = shotSpeed;
        //Dees.GetComponent<Bullet>().damage = shotShotDamage;
        shotCooldownTimer = shotCooldown;
        float offset = Random.Range(-shotInnacuracy, shotInnacuracy);
        Destroy(Dees, shotRange);
        if (Input.GetKey("up"))
        {
            Dees.transform.Rotate(new Vector3(0, 0, 90 + offset));
        }
        else
        if (Input.GetKey("down"))
        {
            Dees.transform.Rotate(new Vector3(0, 0, 270 + offset));
        }
        else
        if (Input.GetKey("right"))
        {
            Dees.transform.Rotate(new Vector3(0, 0, 0 + offset));
        }
        else
        if (Input.GetKey("left"))
        {
            Dees.transform.Rotate(new Vector3(0, 0, 180 + offset));
        }
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetKnockback()
    {
        return knockBack;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            if (other.GetComponent<ItemIdScript>().cost <= coins)
            {
                if (other.GetComponent<ItemIdScript>().isBomb) {
                    bombsLeft++;
                    updateTextElements();
                    Destroy(other.gameObject);
                }
                else
                {
                    coins -= other.GetComponent<ItemIdScript>().cost;
                    int id = other.GetComponent<ItemIdScript>().itemId;
                    bool isStat = other.GetComponent<ItemIdScript>().isStat;
                    GetItem(id, isStat);
                    Destroy(other.gameObject);
                }
            }
        }
        if (other.CompareTag("Coin"))
        {
            coins = coins + other.GetComponent<CoinScript>().value;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EnemyShot") && !deadHard)
        {
            Destroy(other.gameObject);
            health--;
        }
        if (other.CompareTag("EnemyAOE") && !deadHard)
        {
            health--;
        }
        if (other.CompareTag("Explosion") && !deadHard)
        {
            health --;
        }
    }
    void GetItem(int id, bool isStat)
    {
        if (isStat)
        {
            switch (id)
            {
                case 1:
                    speed = speed * 1.1f;
                    break;
                case 2:
                    damage *= 1.2f;
                    break;
                case 3:
                    shotCooldown *= 0.82f;
                    break;
                default:
                    break;
            }
        }
        if (!isStat)
        {
            switch (id)
            {
                case 1:
                    hasDualWield = true;
                    break;
                case 2:
                    canFly = true;
                    break;
                case 3:
                    transform.localScale = transform.localScale / 2;
                    break;
                case 4:
                    bombBullets = true;
                    shotCooldown += 20;
                    break;
                default:
                    break;
            }
        }
    }
    /*
    void updateHealthBar()
    {
        healthBar.transform.position = new Vector3(healthBarStartPos.x - ((maxHealth - health) / maxHealth * defaultHealthbarSizex), healthBarStartPos.y, 0);
        //Debug.Log(((maxHealth - health) / maxHealth * defaultHealthbarSizex));
        healthBar.transform.localScale = new Vector3(defaultHealthbarSizex - ((maxHealth - health) / maxHealth * defaultHealthbarSizex), defaultHealthbarSizey, 1.0f);
        if (health < 0)
            healthBar.transform.localScale = new Vector3(0, 0, 0);
    }
    */
    void updateHealthBarMask()
    {
        healthbarMask.transform.position = maskStart + new Vector3(((maxHealth - health) / maxHealth * defaultHealthbarSizex) *-1, 0, 0);
    }
    void checkIsDead()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            //GAMEOVER
            Time.timeScale = 0.0017f;
            if (deathTimer >= 0.01f)
                SceneManager.LoadScene(sceneToLoadOnDeath);
            else
            {
                deathTimer += Time.deltaTime;
                //Debug.Log(deathTimer);
            }

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy") && !deadHard)
        {
            health--;
        }
        

        if (other.collider.CompareTag("Boss") && !deadHard)
        {
            health--;
        }

        if (canFly && other.gameObject.tag == "Rock")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
        }
    }

void updateTextElements()
    {
        bombsText.text = "x " + bombsLeft;
        coinsText.text = "x " + coins;
    }

    void WinCondition()
    {
        if (GameObject.FindGameObjectWithTag("Boss"))
        {
            gameObject.GetComponent<Victory>().enabled = true;
        }
    }
}
/*
      using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerThings : MonoBehaviour
{
    public Sprite Shot;
    public float speed;
    public float health;
    float ySpeed;
    float xSpeed;
    public float maxSpeed;
    public float deCel;
    bool xMoving;
    bool yMoving;
    public float speedReduct = 1000;
    bool deadHard;
    public float dhTimer;
    public float dhLength;
    public float dhCoolDown;
    public float dhMult = 1.5f;
    public GameObject playerShot;
    public float shotCooldownTimer;
    public float shotSpeed = 4;
    public float shotCooldown;
    public float shotInnacuracy = 0;
    public float shotRange = 2;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        xSpeed = 0;
        ySpeed = 0;
        deadHard = false;
        dhTimer = dhCoolDown;
        shotCooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UseDeadHard();
        InputMove();
        Move();
        Decelerate();
        Edge();
        if (!deadHard)
            Shoot();
    }
    public void UseDeadHard()
    {
        if (Input.GetKey(KeyCode.E) && dhTimer <= dhCoolDown)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                deadHard = true;
                dhTimer = dhLength;
                ySpeed = maxSpeed * dhMult;
                yMoving = true;
            }
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                deadHard = true;
                dhTimer = dhLength;
                ySpeed = -maxSpeed * dhMult;
                yMoving = true;
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                deadHard = true;
                dhTimer = dhLength;
                xSpeed = maxSpeed * dhMult;
                xMoving = true;
            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                deadHard = true;
                dhTimer = dhLength;
                xSpeed = -maxSpeed * dhMult;
                xMoving = true;
            }
        }
        if (dhCoolDown < dhTimer)
        {
            dhTimer--;
            if (dhTimer < dhCoolDown)
                dhTimer = dhCoolDown;
            if (0 > dhTimer && deadHard)
            {
                deadHard = false;
                if (ySpeed > maxSpeed)
                    ySpeed = maxSpeed;
                if (ySpeed < -maxSpeed)
                    ySpeed = -maxSpeed;
                if (xSpeed > maxSpeed)
                    xSpeed = maxSpeed;
                if (xSpeed < -maxSpeed)
                    xSpeed = -maxSpeed;
            }
        }
    }
    public void InputMove()
    {

        if (!deadHard)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                ySpeed += speed;
                yMoving = true;
                if (ySpeed > maxSpeed)
                    ySpeed = maxSpeed;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                ySpeed -= speed;
                yMoving = true;
                if (ySpeed < -maxSpeed)
                    ySpeed = -maxSpeed;

            }
            else
            {
                yMoving = false;
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                xSpeed += speed;
                xMoving = true;
                if (xSpeed > maxSpeed)
                    xSpeed = maxSpeed;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                xSpeed -= speed;
                xMoving = true;
                if (xSpeed < -maxSpeed)
                    xSpeed = -maxSpeed;

            }
            else
            {
                xMoving = false;
            }
        }
    }
    public void Move()
    {


        //This handels movement
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(xSpeed / speedReduct, ySpeed / speedReduct);
    }
    public void Decelerate()
    {
        if (!deadHard)
        {
            if (ySpeed > 0 && yMoving == false)
            {
                ySpeed -= deCel;
                if (ySpeed < 0)
                    ySpeed = 0;
            }
            if (ySpeed < 0 && yMoving == false)
            {
                ySpeed += deCel;
                if (ySpeed > 0)
                    ySpeed = 0;
            }
            if (xSpeed > 0 && xMoving == false)
            {
                xSpeed -= deCel;
                if (xSpeed < 0)
                    xSpeed = 0;
            }
            if (xSpeed < 0 && xMoving == false)
            {
                xSpeed += deCel;
                if (xSpeed > 0)
                    xSpeed = 0;
            }
        }
    }
    public void Edge()
    {

        if (this.gameObject.transform.position.x > 8.636)
        {
            xSpeed = 0;
            this.gameObject.transform.position = new Vector3(8.636f, this.gameObject.transform.position.y);
        }
        if (this.gameObject.transform.position.x < -8.636)
        {
            xSpeed = 0;
            this.gameObject.transform.position = new Vector3(-8.636f, this.gameObject.transform.position.y);
        }
        if (this.gameObject.transform.position.y > 4.75)
        {
            ySpeed = 0;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 4.75f);
        }
        if (this.gameObject.transform.position.y < -4.75)
        {
            ySpeed = 0;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -4.75f);
        }
    }
    public void Shoot()
    {
        if (shotCooldownTimer <= 0)
        {

            if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
            {

                GameObject Dees = Instantiate(playerShot, transform.position, transform.rotation) as GameObject;

                Dees.GetComponent<Bullet>().speed = shotSpeed;
                //Dees.GetComponent<Bullet>().damage = shotShotDamage;
                shotCooldownTimer = shotCooldown;
                float offset = Random.Range(-shotInnacuracy, shotInnacuracy);
                Destroy(Dees, shotRange);
                if (Input.GetKey("up"))
                {
                    Dees.transform.Rotate(new Vector3(-1, offset, 0));
                }
                else
                if (Input.GetKey("down"))
                {
                    Dees.transform.Rotate(new Vector3(1, offset, 0));
                }
                else
                if (Input.GetKey("right"))
                {
                    Dees.transform.Rotate(new Vector3(offset, 1, 0));
                }
                else
                if (Input.GetKey("left"))
                {
                    Dees.transform.Rotate(new Vector3(offset, -1, 0));
                }
            }

        }
        else
        {
            shotCooldownTimer--;
            if (shotCooldownTimer < 0)
                shotCooldownTimer = 0;
        }
    }
}
*/




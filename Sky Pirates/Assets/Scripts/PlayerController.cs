using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // speed variables
    public float playerSpeed;
    public float bulletSpeed;

    // game objects
    public Rigidbody2D myRb;
    public GameObject bullet;
    public GameObject cannonBall;
    public Image healthIcon;
    public Image healthBar;

    // lifespans and Timers
    public float bulletLifespan = 2.5f;
    public float ballCooldown = 5;

    // healths
    public int health = 100;
    public int maxHealth = 100;
    public HealthBar healthBarScript;

    // UI text
    public int score = 0;
    public int money = 0;

    // bools
    public bool ballin = true;
    public bool isDead = false;

    // sprites
    public Animator anim;

    /*
    public Sprite playerUp;
    public Sprite playerDown;
    public Sprite playerNeut;
    */
    public Sprite healthy;
    public Sprite hurty;
    public Sprite dead;
    public Sprite explosion;

    // Scripts
    public ParticleSystem smokin;
    // public ShopController shop;
    public dialogueManager dialogue;


    // Start is called before the first frame update
    void Start()
    {
        dialogue = FindObjectOfType<dialogueManager>();
        smokin.Stop();
        myRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(7, 3);

        // MOVEMENT CODE
        Vector2 velocity = myRb.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * playerSpeed;
        velocity.y = Input.GetAxisRaw("Vertical") * playerSpeed;

        myRb.velocity = velocity;


        // SHOOT CODE
        if (Input.GetKeyDown(KeyCode.Mouse0) && !dialogue.inConvo)
        {
            GameObject b = Instantiate(bullet, transform);
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), b.GetComponent<BoxCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            Destroy(b, bulletLifespan);
        }

        // ABILITY CODEs

        if (Input.GetKeyDown(KeyCode.Mouse1) && ballin && !dialogue.inConvo)
        {
            GameObject c = Instantiate(cannonBall, transform);
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), c.GetComponent<CircleCollider2D>());
            c.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            StartCoroutine("cannonCooldown");
            Destroy(c, bulletLifespan);
        }


        // HEALTH CODE

        if (health <= 0 && !isDead)
        {
            StartCoroutine("die");
            isDead = true;
        }

        /*
        if (health == -5)
        {
            StartCoroutine("die");
        }
        */


        // HEALTH BAR CODE

        
        if (health < 90 & health > 70)
        {
            //healthBarScriptImage.GetComponent<Transform>().localScale = new Vector3(12.5f, 0.4438875f, 1);
            healthIcon.GetComponent<Image>().sprite = healthy;
        }
        else if (health < 70 & health > 50)
        {
            //healthBarScriptImage.GetComponent<Transform>().localScale = new Vector3(10.5f, 0.4438875f, 1);
            healthIcon.GetComponent<Image>().sprite = healthy;
        }
        else if (health < 50 & health > 40)
        {
            //healthBarScriptImage.GetComponent<Transform>().localScale = new Vector3(7.5f, 0.4438875f, 1);
            healthIcon.GetComponent<Image>().sprite = hurty;
        }
        else if (health < 25 & health > 5)
        {
            //healthBarScriptImage.GetComponent<Transform>().localScale = new Vector3(4f, 0.4438875f, 1);
            healthIcon.GetComponent<Image>().sprite = dead;
        }
        


        // SPRITE CHANGE CODE
        if (velocity.y > 0)
        {
            //GetComponent<SpriteRenderer>().sprite = playerUp;
        } else if (velocity.y < 0)
        {
            //GetComponent<SpriteRenderer>().sprite = playerDown;
        } else if (velocity.y == 0)
        {
            //GetComponent<SpriteRenderer>().sprite = playerNeut;
            anim.Play("DefaultState");
        }
    }




    // timers

    IEnumerator cannonCooldown ()
    {
        ballin = false;
        Debug.Log("Wait for it");
        yield return new WaitForSeconds(ballCooldown);
        ballin = true;
        Debug.Log("Done");
    }

    IEnumerator ow ()
    {
        // change the player color if it takes damage
        GetComponent<SpriteRenderer>().color = new Vector4(0.8962264f, 0.3255162f, 0.3255162f, 1f);
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
    }

    IEnumerator die()
    {
        smokin.Play();
        yield return new WaitForSeconds(5);
        Debug.Log("DIE");
        Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "eBullet")
        {
            StartCoroutine("ow");
            // Debug.Log("ouchie");
            health = health - 5;
            healthBarScript.UpdateHealthBar(0.05f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "enemyVF" || collision.gameObject.tag == "enemyZZ" || collision.gameObject.tag == "enemySine")
        {
            collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            StartCoroutine("ow");
            health = health - 20;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = explosion;
            healthBarScript.UpdateHealthBar(.20f);
            Destroy(collision.gameObject, 0.5f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            dialogue.cocoInteraction1();
            // Debug.Log("random powerup");
        }
    }

}


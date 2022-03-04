using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // speed variables
    public float playerSpeed;
    public float bulletSpeed;

    // game objects
    public Rigidbody2D myRb;
    public GameObject bullet;
    public GameObject cannonBall;

    // lifespans and Timers
    public float bulletLifespan = 2.5f;
    public float ballCooldown = 5;

    // healths
    public int health = 100;

    // bools
    public bool ballin = true;

    // sprites
    public Sprite playerUp;
    public Sprite playerDown;
    public Sprite playerNeut;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT CODE

        Vector2 velocity = myRb.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * playerSpeed;
        velocity.y = Input.GetAxisRaw("Vertical") * playerSpeed;

        myRb.velocity = velocity;


        // SHOOT CODE

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject b = Instantiate(bullet, transform);
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), b.GetComponent<CapsuleCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            Destroy(b, bulletLifespan);
        }

        // ABILITY CODE

        if (Input.GetKeyDown(KeyCode.Mouse1) & ballin)
        {
            GameObject c = Instantiate(cannonBall, transform);
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), c.GetComponent<CircleCollider2D>());
            c.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            StartCoroutine("cannonCooldown");
            Destroy(c, bulletLifespan);
        }


        // HEALTH CODE

        if (health < 1)
        {
            Destroy(gameObject);
        }


        // SPRITE CHANGE CODE
        if (velocity.y > 0)
        {
            GetComponent<SpriteRenderer>().sprite = playerUp;
        } else if (velocity.y < 0)
        {
            GetComponent<SpriteRenderer>().sprite = playerDown;
        } else if (velocity.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = playerNeut;
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




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "eBullet")
        {
            StartCoroutine("ow");
            Debug.Log("ouchie");
            health = health - 5;
            Destroy(collision.gameObject);
        }
    }

}


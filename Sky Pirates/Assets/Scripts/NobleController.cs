using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobleController : MonoBehaviour
{
    public float ballSpeed = 7.0f;
    public float ballLifespan = 5f;
    public float rateOfFire = 0.5f;
    public float rofTimer = 0;
    public int bulletCount = 0;

    public int health = 25;
    public Sprite explode;

    public bool canShoot = true;
    public bool readyToShoot = false;
    public bool isDead = false;

    public GameObject ball;
    public GameObject player;
    public GameObject InvisWall;

    private Animator anim;
    private AudioSource speaker;

    public LayerMask mask;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvisWall = GameObject.FindGameObjectWithTag("InvisWall");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(6, 8);
        Physics2D.IgnoreLayerCollision(6, 3);
        Physics2D.IgnoreLayerCollision(6, 11);
        Physics2D.IgnoreLayerCollision(6, 12);
        Physics2D.IgnoreLayerCollision(8, 3);
        Physics2D.IgnoreLayerCollision(8, 6);
        Physics2D.IgnoreLayerCollision(8, 12);

        if (canShoot & readyToShoot & bulletCount < 3 & !isDead)
        {
            GameObject b = Instantiate(ball, transform.position, Quaternion.Euler(0, 0, 0));
            bulletCount++;
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<BoxCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-ballSpeed, 0);

            canShoot = false;

            Destroy(b, ballLifespan);

            if (bulletCount == 3)
            {
                StartCoroutine("bulletCooldown");
            }
        }

        if (canShoot == false && rofTimer < rateOfFire & readyToShoot)
        {
            rofTimer += Time.deltaTime;

            if (rofTimer >= rateOfFire)
            {
                canShoot = true;
                rofTimer = 0;
            }
        }

        // DIE CODE
        if (health <= 0 && !isDead)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            player.GetComponentInParent<PlayerController>().score += 10;
            isDead = true;
            speaker.Play();
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            anim.SetBool("dead", true);
            Destroy(gameObject, 1f);
        }
    }


    // bullet shoot interval
    IEnumerator bulletCooldown()
    {
        //Debug.Log("Wait for it");
        yield return new WaitForSeconds(5);
        bulletCount = 0;
    }

    /*
    IEnumerator dead()
    {
        // Debug.Log("explosion")
        isDead = true;
        speaker.Play();
        gameObject.GetComponent<SpriteRenderer>().sprite = explode;
        yield return new WaitForSeconds(0.3f);
        // Debug.Log("exploded");
        Destroy(gameObject, );
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "pBullet")
        {
            health = health - 10;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "pCannon")
        {
            health = health - 20;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ShootZone")
        {
            readyToShoot = true;
        }
    }
}

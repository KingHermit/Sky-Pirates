using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobleController : MonoBehaviour
{
    public float ballSpeed = 7.0f;
    public float ballLifespan = 2.5f;
    public float rateOfFire = 0.5f;
    public float rofTimer = 0;
    public int bulletCount = 0;

    public int health = 25;

    public bool canShoot = true;
    public bool readyToShoot = false;

    public GameObject ball;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot & readyToShoot & bulletCount < 3)
        {
            GameObject b = Instantiate(ball, transform);
            bulletCount++;
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<CapsuleCollider2D>());
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
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }



    // bullet shoot interval
    IEnumerator bulletCooldown ()
    {
        Debug.Log("Wait for it");
        yield return new WaitForSeconds(2);
        bulletCount = 0;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "pBullet")
        {
            Debug.Log("OWWWW");
            health = health - 10;
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

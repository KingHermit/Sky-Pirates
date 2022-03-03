using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobleController : MonoBehaviour
{
    public float ballSpeed = 7.0f;
    public float ballLifespan = 2.5f;
    public float rateOfFire = 2f;
    public float rofTimer = 0;

    public bool canShoot = true;

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
        if (canShoot)
        {
            GameObject b = Instantiate(ball, transform);
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), b.GetComponent<CapsuleCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-ballSpeed, 0);

            canShoot = false;

            Destroy(b, ballLifespan);
        }
        
        if (canShoot == false && rofTimer < rateOfFire)
        {
            rofTimer += Time.deltaTime;

            if (rofTimer >= rateOfFire)
            {
                canShoot = true;
                rofTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "cannon ball")
        {
            Destroy(collision.gameObject);
        }
    }
}

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

    // lifespans and Timers
    public float bulletLifespan = 2.5f;

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

        // MOVEMENT CODE


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject b = Instantiate(bullet, transform);
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), b.GetComponent<CapsuleCollider2D>());
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            Destroy(b, bulletLifespan);
        }

    }
}

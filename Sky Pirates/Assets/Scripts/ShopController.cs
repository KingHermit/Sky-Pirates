using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public WaveManager3 waveManager;

    public Animator anim;

    private float moveSpeed = 2;
    public float leftBound = -12;
   

    // Is it open or closed
    // public bool isOpen = false;
    public bool isClosed = false;

    public Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        waveManager = GameObject.FindGameObjectWithTag("WaveM").GetComponent<WaveManager3>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 3);

        if (isClosed)
        {
            anim.SetBool("ShopClosed", true);
            Physics2D.IgnoreLayerCollision(9, 10);
            Physics2D.IgnoreLayerCollision(7, 10);
            moveSpeed = 3;
            Debug.Log("Closed");
        }
        else if (!isClosed)
        {
            anim.SetBool("ShopClosed", false);
            Physics2D.IgnoreLayerCollision(7, 10);
            moveSpeed = 2;
            Debug.Log("Open");
        }

        myRB.velocity = new Vector2(-moveSpeed, 0);

        if (transform.position.x < leftBound)
        {
            isClosed = false;
            Destroy(gameObject);
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 0;
            Debug.Log("Don't hit my store!");
            // GetComponent<BoxCollider2D>().isTrigger = true;
            // isClosed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 3;
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 0;
            // Debug.Log("Don't hit my store!");
            // GetComponent<BoxCollider2D>().isTrigger = true;
            // isClosed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 3;
            isClosed = true;
            //CannonBallCooldown();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public WaveManager3 waveManager;

    public Sprite closed;
    public Sprite open;

    private float moveSpeed = 2;
    public float leftBound = -12;

    // Is it open or closed
    public bool isOpen = false;
    public bool isClosed = false;

    public Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        waveManager = GameObject.FindGameObjectWithTag("WaveM").GetComponent<WaveManager3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClosed && !isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = closed;
            Physics2D.IgnoreLayerCollision(9, 10);
            Physics2D.IgnoreLayerCollision(7, 10);
            moveSpeed = 3;
        }
        else if (isOpen && !isClosed)
        {
            GetComponent<SpriteRenderer>().sprite = open;
            moveSpeed = 2;
        }

        myRB.velocity = new Vector2(-moveSpeed, 0);

        if (transform.position.x < leftBound)
        {
            isOpen = false;
            isClosed = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 0;
            Debug.Log("Don't hit my store!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 3;
        }
    }
}

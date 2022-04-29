using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{
    private float moveSpeed = 1;
    public float leftBound = -12;
    public bool inShootZone = false;

    public Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(3, 12);

        if (gameObject.name == "placeholder bg")
        {
            moveSpeed = 1;
        }

        // Air Mines
        if (gameObject.tag == "airMine")
        {
            moveSpeed = 1;
        }

        // Basic Enemy
        if (gameObject.tag == "enemy" & !inShootZone)
        {
            moveSpeed = 2f;
        } else if (gameObject.tag == "enemy" & inShootZone)
        {
            moveSpeed = 1.5f;
        }
        

        myRB.velocity = new Vector2(-moveSpeed, 0);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ShootZone")
        {
            inShootZone = true;
        }
    }
}

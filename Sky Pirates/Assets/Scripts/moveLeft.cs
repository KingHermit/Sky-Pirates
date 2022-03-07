using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{
    private float moveSpeed = 1;
    public float leftBound = -75;
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
        if (gameObject.name == "placeholder bg")
        {
            moveSpeed = 1;
        }

        if (gameObject.tag == "enemy" & !inShootZone)
        {
            moveSpeed = 2f;
        } else if (gameObject.tag == "enemy" & inShootZone)
        {
            moveSpeed = 0.6f;
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

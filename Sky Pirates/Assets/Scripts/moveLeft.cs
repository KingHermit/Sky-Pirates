using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{
    public float moveSpeed = 1;
    public float leftBound = -75;

    public Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = new Vector2(-moveSpeed, 0);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}

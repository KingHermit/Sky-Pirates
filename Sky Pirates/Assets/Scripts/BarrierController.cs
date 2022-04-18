using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public WaveManager4 waves;

    private float moveSpeed = 1;

    public Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();

        waves = GameObject.FindGameObjectWithTag("WaveM").GetComponent<WaveManager4>();
    }

    // Update is called once per frame
    void Update()
    {
        rB.velocity = new Vector2(-moveSpeed, 0);

        if (transform.position.x <= 3.5f)
        {
            moveSpeed = 0;
        }

        if (waves.dstryWall == true)
        {
            Destroy(gameObject);
        }
    }
}

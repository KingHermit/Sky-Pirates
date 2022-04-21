using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlimpController : MonoBehaviour
{
    public WaveManager4 waves;

    // Movement Speed To Location
    private float moveSpeed = 1f;

    // Health
    public int health = 500;

    // Enemy Wave Formations Before Blimp is Vulnerable
    public int enemyWaveForm;

    // Booleans
    public bool inPosition = false;
    public bool isVulnerable = false;
    public bool noblesReady = false;
    public bool isDead = false;
    public bool searchingForEnemies = false;

    public Sprite explosion;
    public SpriteRenderer vulnerablity;
    public GameObject player;

    // Nobles
    public Transform enemyVF;

    // Nobles SP
    public Transform topY3;
    public Transform tMidY2;
    public Transform headY0;
    public Transform bMidY2;
    public Transform bottomY3;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        waves = GameObject.FindGameObjectWithTag("WaveM").GetComponent<WaveManager4>();

        topY3 = GameObject.FindGameObjectWithTag("topY3").transform;
        tMidY2 = GameObject.FindGameObjectWithTag("tMidY2").transform;
        headY0 = GameObject.FindGameObjectWithTag("headY0").transform;
        bMidY2 = GameObject.FindGameObjectWithTag("bMidY2").transform;
        bottomY3 = GameObject.FindGameObjectWithTag("bottomY3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(11, 3);

        // Moving to Position
        if (inPosition == false)
        {
            Physics2D.IgnoreLayerCollision(6, 11, true);
            Physics2D.IgnoreLayerCollision(7, 11, true);
            Physics2D.IgnoreLayerCollision(8, 11, true);
            Physics2D.IgnoreLayerCollision(9, 11, true);
            vulnerablity.color = new Color(1f, 1f, 1f, 1f);
        }

        // Is Able to Damage Blimp
        if (!isVulnerable && inPosition)
        {
            vulnerablity.color = new Color(.6f, .6f, .6f, 1f);
        }
        else if (isVulnerable && inPosition)
        {
            vulnerablity.color = new Color(1f, 1f, 1f, 1f);
        }

        if (searchingForEnemies == false /* bool switched on after all enemies gone*/)
        {
            if (GameObject.FindGameObjectWithTag("enemyVF") == null)
            {
                // Problem with isVulnerable and noblesReady
                StartCoroutine(EnemyVulnerable());
                searchingForEnemies = true;
                StartCoroutine(WaitMore());
            }

            /* switch bool off after above statement runs */
            //StartCoroutine(WaitMore());
        }

        // DIE CODE
        if (health < 1 & !isDead)
        {
            player.GetComponentInParent<PlayerController>().score += 100;
            StartCoroutine("Dead");
        }

        if (inPosition == false)
        {
            if (transform.position.x <= 14)
            {
                moveSpeed = 0;
                inPosition = true;

                rB.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
        else
        {
            return;
        }

        rB.velocity = new Vector2(-moveSpeed, 0);
    }

    IEnumerator Dead()
    {
        // Debug.Log("explosion")
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = explosion;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    IEnumerator EnemyVulnerable()
    {
        //isVulnerable = true;
        vulnerablity.color = new Color(1f, 1f, 1f, 1f);
        Physics2D.IgnoreLayerCollision(6, 11, true);
        Physics2D.IgnoreLayerCollision(7, 11, false);
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(9, 11, false);

        isVulnerable = true;
        noblesReady = false;

        yield return new WaitForSeconds(5f);

        //isVulnerable = false;

        vulnerablity.color = new Color(.6f, .6f, .6f, 1f);
        Physics2D.IgnoreLayerCollision(6, 11, true);
        Physics2D.IgnoreLayerCollision(7, 11, true);
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);

        isVulnerable = false;
        noblesReady = true;

        BlimpPilotsReady();
    }

    IEnumerator WaitMore()
    {
        yield return new WaitForSeconds(7f);
        searchingForEnemies = false;
    }

    IEnumerator PilotsReady()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnFlyingV(enemyVF);
            yield return new WaitForSeconds(8f);
        }

        Debug.Log("Repeating");
    }

    void BlimpPilotsReady()
    {
        // Spawn
        if (isVulnerable == false || noblesReady == true)
        {
            StartCoroutine(PilotsReady());
        }
    }

    void SpawnFlyingV(Transform _enemy)
    {
        Instantiate(_enemy, topY3.position, transform.rotation);
        Instantiate(_enemy, tMidY2.position, transform.rotation);
        Instantiate(_enemy, headY0.position, transform.rotation);
        Instantiate(_enemy, bMidY2.position, transform.rotation);
        Instantiate(_enemy, bottomY3.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "pBullet")
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
}

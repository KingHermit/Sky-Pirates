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

    public Sprite explosion;
    public SpriteRenderer vulnerablity;
    public GameObject player;

    Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        waves = GameObject.FindGameObjectWithTag("WaveM").GetComponent<WaveManager4>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Physics2D.IgnoreLayerCollision(6, 11, true);
            Physics2D.IgnoreLayerCollision(7, 11, true);
            Physics2D.IgnoreLayerCollision(8, 11, true);
            Physics2D.IgnoreLayerCollision(9, 11, true);

            noblesReady = true;
        }
        else if (isVulnerable && inPosition)
        {
            vulnerablity.color = new Color(1f, 1f, 1f, 1f);
            Physics2D.IgnoreLayerCollision(6, 11, true);
            Physics2D.IgnoreLayerCollision(7, 11, false);
            Physics2D.IgnoreLayerCollision(8, 11, true);
            Physics2D.IgnoreLayerCollision(9, 11, false);

            noblesReady = false;
        }

        if (GameObject.FindGameObjectWithTag("enemyVF") == null)
        {
            StartCoroutine(EnemyVulnerable());
        }

        // DIE CODE
        if (health < 1 & !isDead)
        {
            player.GetComponentInParent<PlayerController>().score += 100;
            StartCoroutine("Dead");
        }

        if(inPosition == false)
        {
            if (transform.position.x <= 14)
            {
                moveSpeed = 0;
                inPosition = true;
                isVulnerable = false;

                rB.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
        else
        {
            Debug.Log("Not repeating position");
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
        isVulnerable = true;
        yield return new WaitForSeconds(5f);
        isVulnerable = false;
    }

    /*
    IEnumerator FadeIn(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            vulnerablity.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        vulnerablity.color = end; //without this, the value will end at something like 0.9992367
    }
    */

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

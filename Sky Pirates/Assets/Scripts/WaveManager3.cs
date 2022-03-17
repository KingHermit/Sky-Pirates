using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager3 : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    /*
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    */

    public GameObject shopGO;
    
    // Shop state
    public Sprite[] shopState;
    
    
    // Shop spawn var
    public Transform shop;
    public Transform shopSpawn;

    // Enemy Var
    public Transform enemy;
    public int count;
    public float rate;

    // Enemy Spawn
    public Transform[] spawnPoints;

    // Wave number for UI
    public int currentWaveNumber = 0;

    // Time before next wave
    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    // Checks if enemy is present in scene
    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0 && !ShopIsHere())
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning wave
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        SpawnShop();

        if(count >= 15)
        {
            return;
        }
        else
        {
            count = count + 2;
        }

        if (rate >= 5)
        {
            return;
        }
        else
        {
            rate = rate + 0.5f;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    bool ShopIsHere()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Shop") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave()
    {
        currentWaveNumber++;

        if (currentWaveNumber % 5 == 0)
        {
            Debug.Log("Boss Wave!");
        }
        else
        {
            Debug.Log("Incoming Wave!");

            state = SpawnState.SPAWNING;

            // Spawn
            for (int i = 0; i < count; i++)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(10f / rate);
            }

            state = SpawnState.WAITING;
        }

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, transform.rotation);
    }

    void SpawnShop()
    {
        int state = Random.Range(1, 10);

        Instantiate(shop, shopSpawn.position, transform.rotation);

        ShopController _sc = shopGO.GetComponent<ShopController>();

        if (state >= 9)
        {
            _sc.isClosed = true;
            _sc.isOpen = false;
        }
        else if (state <= 8)
        {
            _sc.isClosed = false;
            _sc.isOpen = true;
        }
    }
}

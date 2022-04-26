using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager4 : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public GameObject shopGO;

    public BlimpController blimpController;

    // Shop spawn var
    public Transform shop;
    public Transform shopSpawn;

    // Enemy Var
    public Transform enemy;
    public Transform enemySine;
    public Transform enemyVF;
    public Transform enemyZZ;
    public Transform enemyBoss;
    public Transform airMines;
    public int count;
    public float rate;

    // Day & Night Cycle
    public GameObject background;
    public GameObject background2;
    public Color bgCycle;
    public Color day;
    public Color night;
    public bool isNight = false;

    // Enemy Spawn
    public Transform[] spawnPoints;

    // Flying V Enemy Spawn
    public Transform topY3;
    public Transform tMidY2;
    public Transform headY0;
    public Transform bMidY2;
    public Transform bottomY3;

    // ZigZag Enemy Spawn
    public Transform leadZZY2;
    public Transform midZZY0;
    public Transform bottomZZY2;
    public Transform endZZY0;

    // Wavy Enemy Spawn
    public Transform sineSpawnPoint;

    // Boss Blimp Spawn
    public Transform bbSpawnPoint;

    // Wave number for UI
    public int currentWaveNumber = 0;
    public int waveFormCount = 0;

    // Time before next wave
    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    // Checks if enemy is present in scene
    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    public dialogueManager dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue.inConvo = true;

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        waveFormCount = currentWaveNumber;

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

        if (waveCountdown <= 0 && !ShopIsHere() && !dialogue.inConvo)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning wave
                StartCoroutine(SpawnWave());
                Debug.Log("Air mine deploying!");
                StartCoroutine(SpawnAirMines());
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Complete Wave " + currentWaveNumber + "!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        SpawnShop();

        if (currentWaveNumber % 12 == 0)
        {
            StartCoroutine(TimeCycle(night, day, 5f));
        }
        else if (currentWaveNumber % 6 == 0)
        {
            StartCoroutine(TimeCycle(day, night, 5f));
        }
        else
        {
            return;
        }

        if (waveFormCount >= 6)
        {
            waveFormCount = 5;
            return;
        }
        else
        {
            waveFormCount = currentWaveNumber;
        }

        if (count >= 15)
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

    public bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("airMine") == null && GameObject.FindGameObjectWithTag("enemy") == null && GameObject.FindGameObjectWithTag("BossBlimp") == null && GameObject.FindGameObjectWithTag("enemyVF") == null && GameObject.FindGameObjectWithTag("enemyZZ") == null && GameObject.FindGameObjectWithTag("enemySine") == null)
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
            BossWaveReady();
        }
        else if (currentWaveNumber % 3 == 0)
        {
            Debug.Log("Incoming Wave " + currentWaveNumber + ": Flying V");

            state = SpawnState.SPAWNING;

            // Spawn
            for (int i = 0; i < waveFormCount; i++)
            {
                SpawnFlyingV(enemyVF);
                yield return new WaitForSeconds(10f);
            }

            state = SpawnState.WAITING;
        }
        else if (currentWaveNumber % 4 == 0)
        {
            Debug.Log("Incoming Wave " + currentWaveNumber + ": Zig-Zag");

            state = SpawnState.SPAWNING;

            // Spawn
            for (int i = 0; i < waveFormCount; i++)
            {
                SpawnZigZag(enemyZZ);
                yield return new WaitForSeconds(8f);
            }

            state = SpawnState.WAITING;
        }
        else if (currentWaveNumber % 7 == 0)
        {
            Debug.Log("Incoming Wave " + currentWaveNumber + ": Wavy");

            state = SpawnState.SPAWNING;

            // Spawn
            for (int i = 0; i < count; i++)
            {
                SpawnSineWave(enemySine);
                yield return new WaitForSeconds(10f / rate);
            }

            state = SpawnState.WAITING;
        }
        else
        {
            Debug.Log("Incoming Wave " + currentWaveNumber + ": Normal");

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
    
    IEnumerator SpawnAirMines()
    {
        if (currentWaveNumber >= 6)
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnMines(airMines);
                Debug.Log("Mines Deployed");
                yield return new WaitForSeconds(4f);
            }
        }
    }

    IEnumerator TimeCycle(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            background.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            background2.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        bgCycle = end; //without this, the value will end at something like 0.9992367
        background.GetComponent<SpriteRenderer>().color = bgCycle;
        background2.GetComponent<SpriteRenderer>().color = bgCycle;
    }

    void BossWaveReady()
    {
        Debug.Log("Boss Wave!");
        
        SpawnBlimp(enemyBoss);

        state = SpawnState.WAITING;
    }

    void SpawnShop()
    {
        int state = Random.Range(1, 10);

        Instantiate(shop, shopSpawn.position, transform.rotation);

        ShopController _sc = shopGO.GetComponent<ShopController>();

        if (state >= 8)
        {
            _sc.isClosed = true;
            // _sc.isOpen = false;
        }
        else if (state <= 7)
        {
            _sc.isClosed = false;
            // _sc.isOpen = true;
        }
    }

    void SpawnMines(Transform _mines)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_mines, _sp.position, transform.rotation);
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, transform.rotation);
    }

    void SpawnZigZag(Transform _enemy)
    {
        Instantiate(_enemy, leadZZY2.position, transform.rotation);
        Instantiate(_enemy, midZZY0.position, transform.rotation);
        Instantiate(_enemy, bottomZZY2.position, transform.rotation);
        Instantiate(_enemy, endZZY0.position, transform.rotation);
    }

    void SpawnFlyingV(Transform _enemy)
    {
        Instantiate(_enemy, topY3.position, transform.rotation);
        Instantiate(_enemy, tMidY2.position, transform.rotation);
        Instantiate(_enemy, headY0.position, transform.rotation);
        Instantiate(_enemy, bMidY2.position, transform.rotation);
        Instantiate(_enemy, bottomY3.position, transform.rotation);
    }

    void SpawnSineWave(Transform _enemy)
    {
        Instantiate(_enemy, sineSpawnPoint.position, transform.rotation);
    }

    void SpawnBlimp(Transform _blimp)
    {
        Transform tempBlimp = Instantiate(_blimp, bbSpawnPoint.position, transform.rotation);
        blimpController = tempBlimp.gameObject.GetComponent<BlimpController>();
    }
}

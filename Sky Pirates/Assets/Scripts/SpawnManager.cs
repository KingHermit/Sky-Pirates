using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject noble;

    private int spawnPosX = 17;
    private int spawnRangeY = 3;
    private int startDelay = 3;
    private int spawnInterval = 9;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(17, Random.Range(-spawnRangeY, spawnRangeY));

        Instantiate(noble, spawnPos, noble.transform.rotation);
    }
}

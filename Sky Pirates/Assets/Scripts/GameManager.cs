using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public WaveManager2 waves;
    public Text scoreText;
    public Text moneyText;
    public Text waveText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + player.score;
        moneyText.text = "Coins: " + player.money;
        waveText.text = "Wave " + waves.currentWaveNumber;
    }
}
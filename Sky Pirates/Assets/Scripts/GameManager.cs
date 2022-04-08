using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    // public WaveManager3 waves;
    public WaveManager4 waves;
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            scoreText.text = "Score: " + player.score;
            moneyText.text = "Coins: " + player.money;
            waveText.text = "Wave " + waves.currentWaveNumber;
        }

    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void highScore()
    {
        SceneManager.LoadScene(2);
    }

    public void creditsScene()
    {

        SceneManager.LoadScene(3);

    }
}
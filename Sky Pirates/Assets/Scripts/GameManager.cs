using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public dialogueManager dm;
    public GameObject textBox;
    // public WaveManager3 waves;
    public WaveManager4 waves;
    public Text scoreText;
    public Text waveText;

    public Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            dm.stratusInteraction1();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            
        }

        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scoreText.text = "Score: " + player.score;
            waveText.text = "Wave " + waves.currentWaveNumber;

            if (!dm.inConvo)
            {
                textBox.SetActive(false);
            }
            else
            {
                textBox.SetActive(true);
            }

            if (player.deadForGood)
            {
                SceneManager.LoadScene(3);
            }
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

    public void creditsScene()
    {

        SceneManager.LoadScene(2);

    }
}
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


    // Start is called before the first frame update
    void Start()
    {
        dm.stratusInteraction1();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            scoreText.text = "Score: " + player.score;
            waveText.text = "Wave " + waves.currentWaveNumber;
        }

        if (!dm.inConvo)
        {
            textBox.SetActive(false);
        } else
        {
            textBox.SetActive(true);
        }

        if (player.isDead)
        {
            SceneManager.LoadScene(4);
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
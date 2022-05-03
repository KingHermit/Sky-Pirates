using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject Bandanit;
    public dialogueManager dm;
    public GameObject textBox;
    // public WaveManager3 waves;
    public WaveManager4 waves;
    public Text scoreText;
    // public Text otherScoreText;
    public Text waveText;

    public Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        Bandanit = GameObject.Find("Bandanit");

        if (SceneManager.GetActiveScene().buildIndex == 1)
            dm.stratusInteraction1();

        player = GameObject.Find("Bandanit").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            player.enabled = false;
        }

        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            player.enabled = true;
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

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            player.enabled = false;
            finalScore.text = player.score.ToString();
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void startGame()
    {
        Destroy(Bandanit);
        SceneManager.LoadScene(1);
        player.score = 0;
    }

    public void creditsScene()
    {

        SceneManager.LoadScene(2);

    }
}
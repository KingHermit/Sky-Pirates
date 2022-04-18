using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public dialogueTrigger dialogue;
    public GameObject textBox;
    // public WaveManager3 waves;
    public WaveManager4 waves;
    public Text scoreText;
    public Text waveText;


    // Start is called before the first frame update
    void Start()
    {
        dialogue.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            scoreText.text = "Score: " + player.score;
            waveText.text = "Wave " + waves.currentWaveNumber;
        }

        if (!dialogue.inConvo)
        {
            textBox.SetActive(false);
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
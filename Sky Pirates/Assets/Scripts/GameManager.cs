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
    public Image arrow;
    public Vector3 arrowUp = new Vector3 (800, 450, 0);
    public Vector3 arrowDown = new Vector3 (800, 350, 0);
    private bool up = false;
    public Text finalScore;

    public AudioSource speaker;
    public AudioClip battle;
    public AudioClip theme;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();

        Bandanit = GameObject.Find("Bandanit");

        if(Bandanit != null)
            player = Bandanit.GetComponent<PlayerController>();

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            arrowUp = GameObject.Find("play").transform.position;

            arrowUp.x += 350;

            arrowDown = GameObject.Find("creditsButton").transform.position;

            arrowDown.x += 350;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
            dm.stratusInteraction1();

        if(SceneManager.GetActiveScene().buildIndex == 1)
            player = GameObject.Find("Bandanit").GetComponent<PlayerController>();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            speaker.clip = battle;
            speaker.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex != 1 & SceneManager.GetActiveScene().buildIndex != 3)
        {
            speaker.clip = theme;
            speaker.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Cursor.visible = false;
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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(0);
            }
        }

        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (Input.GetKeyDown(KeyCode.S))
            {
                up = false;
                arrow.rectTransform.transform.position = arrowDown;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                up = true;
                arrow.rectTransform.transform.position = arrowUp;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) & up)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Destroy(Bandanit);
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) & !up)
            {
                SceneManager.LoadScene(2);
            }
        }
        else { }
    }

    // discarded button controls

    /*
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
    */
}
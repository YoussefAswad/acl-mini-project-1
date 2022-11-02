using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public AudioSource gameMusicSource;
    public AudioSource pauseMusicSource;
    public GameObject healthText;
    public GameObject abilityText;
    public GameObject scoreText;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject pauseButton;
    public GameObject abilityButton;
    public GameObject touchControls;
    public GameObject upButton;
    public GameObject gameOverText;
    public GameObject finalScoreText;
    public Camera mainCamera;
    private bool paused = false;
    private bool gameOver = false;
    private bool isAndroid;
    void Start()
    {
        Time.timeScale = 1;
        isAndroid = false;
        Debug.Log(Application.platform);
        if (Application.platform == RuntimePlatform.Android)
        {
            isAndroid = true;
            mainCamera.fieldOfView = 90;
        }
        touchControls.SetActive(isAndroid);

    }

    // Update is called once per frame
    void Update()
    {
        healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + player.GetComponent<PlayerScript>().healthPoints;
        abilityText.GetComponent<TextMeshProUGUI>().text = "Ability: " + player.GetComponent<PlayerScript>().abilityPoints;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + player.GetComponent<PlayerScript>().scorePoints;

        if (player.GetComponent<PlayerScript>().healthPoints <= 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //DestroyAll("Obstacle");
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        Debug.Log(Time.timeScale);
    }


    void DestroyAll(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ReloadScene()
    {

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void Resume()
    {
        if (!gameOver)
        {
            Time.timeScale = 1;
            paused = false;
            resumeButton.SetActive(false);
            restartButton.SetActive(false);
            mainMenuButton.SetActive(false);
            healthText.SetActive(true);
            abilityText.SetActive(true);
            scoreText.SetActive(true);
            pauseButton.SetActive(true);
            abilityButton.SetActive(true);
            upButton.SetActive(true);
            touchControls.SetActive(isAndroid);
            gameMusicSource.mute = false;
            pauseMusicSource.mute = true;
        }
    }

    public void Pause()
    {
        if (!gameOver)
        {
            Time.timeScale = 0;
            paused = true;
            resumeButton.SetActive(true);
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
            healthText.SetActive(false);
            abilityText.SetActive(false);
            scoreText.SetActive(false);
            pauseButton.SetActive(false);
            abilityButton.SetActive(false);
            upButton.SetActive(false);
            touchControls.SetActive(false);
            gameMusicSource.mute = true;
            pauseMusicSource.mute = false;
        }
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        resumeButton.SetActive(false);
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
        healthText.SetActive(false);
        abilityText.SetActive(false);
        scoreText.SetActive(false);
        pauseButton.SetActive(false);
        abilityButton.SetActive(false);
        upButton.SetActive(false);
        gameOverText.SetActive(true);
        finalScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + player.GetComponent<PlayerScript>().scorePoints;
        finalScoreText.SetActive(true);
        touchControls.SetActive(false);
        gameMusicSource.mute = true;
        pauseMusicSource.mute = false;
    }

}

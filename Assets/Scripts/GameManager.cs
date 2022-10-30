using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public bool gameOver = false;
    public bool paused = false;
    public event System.Action OnPause;
    public GameObject pauseMenu;

    public GameObject victoryPanel;
    public TextMeshProUGUI clockText;

    public WaveManager waveManager;


    // FOR TESTING
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        waveManager.GameFinished += GameWon;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true) {
            return;
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(waveManager.gameTimer);
        string time = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        clockText.text = time;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame(!paused);
        }


        // FOR TESTING
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject enemy = Instantiate(enemyPrefabs[0], spawnPoints[0]);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            player.GetComponent<ExpHandler>().LevelUp();
        }
    }




    public void GameWon() {
        // Game Won
        victoryPanel.SetActive(true);

        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        foreach (EnemyController enemy in enemies) {
            Destroy(enemy.gameObject);
        }

        gameOver = true;
    }

    public void PauseGame(bool pause) {
        paused = pause;
        
        if (paused == true) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            if (OnPause != null) {
                OnPause();
            }
        } else {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ResumeGame() {
        PauseGame(false);
    }

    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    public string menuScene = "MainMenu";

    public GameObject deadPanel;

    public PlayerStatsHandler playerStats;

    void Start() {
        playerStats.OnDeath += PlayerDeath;
    }

    public void RestartGame() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame() {
        SceneManager.LoadScene(menuScene);
    }

    public void PlayerDeath(BaseController controller) {
        deadPanel.SetActive(true);
        GameManager.instance.gameOver = true;
    }
}

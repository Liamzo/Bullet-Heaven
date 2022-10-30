using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    public GameObject player;
    public LevelUpPanel levelUpPanel;

    public PlayerStatsHandler playerStats;
    public ExpHandler playerExp;

    public Slider healthBar;
    public Slider expBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI expText;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.OnUIChange += UpdateHealth;
        playerExp.OnUIChange += UpdateExp;
        levelUpPanel.LevelCompleteAction += UpdateExp;

        UpdateHealth();
        UpdateExp();
    }

    void UpdateHealth() {
        healthBar.maxValue = playerStats.stats[(int)PlayerStats.MaxHP].GetValue();

        healthBar.value = playerStats.curHealth;

        healthText.text = healthBar.value + " / " + healthBar.maxValue;
    }

    void UpdateExp() {
        expBar.maxValue = playerExp.expNextLevel;
        expBar.value = playerExp.exp;

        expText.text = "LV. " + playerExp.level;

        UpdateHealth();
    }
}

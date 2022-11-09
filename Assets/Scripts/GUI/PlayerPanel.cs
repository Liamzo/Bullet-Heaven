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
    public EquipmentHandler playerEquipment;

    public Slider healthBar;
    public Slider expBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI expText;

    public Image[] weaponSlots;
    public Sprite emptySlotImage;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.OnUIChange += UpdateHealth;
        playerExp.OnUIChange += UpdateExp;
        levelUpPanel.LevelCompleteAction += UpdateExp;
        playerEquipment.OnUIChange += UpdateEquipment;

        expBar.maxValue = playerExp.expNextLevel;
        expBar.value = playerExp.exp;

        expText.text = "LV. " + playerExp.level;
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

    void UpdateEquipment() {
        int i = 0;
        for (i = 0; i < playerEquipment.weapons.Count; i++) {
            weaponSlots[i].sprite = playerEquipment.weapons[i].GetComponent<Weapon>().sprite;
        }
        for (int j = i; j < weaponSlots.Length; j++) {

            weaponSlots[j].sprite = emptySlotImage;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpHandler : MonoBehaviour
{
    public int level = 0;
    public int exp = 0;
    public int expNextLevel = 10;

    public int levelScalingBase = 5;
    public int levelScaling = 5;
    public int levelIncrement = 10;

    public Collider2D pickupRange;

    public event System.Action OnUIChange;
    public event System.Action OnLevelUp;
    public List<LevelOption> levelOptions = new List<LevelOption>();

    // Audio
    public AudioSource audioSource;
    public AudioClip[] audioClips;


    void Start() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Exp") {
            Destroy(other.gameObject);

            audioSource.PlayOneShot(audioClips[0]);

            exp += 1;

            if (exp >= expNextLevel) {
                LevelUp();
            }

            if (OnUIChange != null) {
                OnUIChange();
            }
        }
    }

    public void LevelUp() {
        level += 1;
        exp = 0;

        levelIncrement += levelScaling;
        levelScaling += levelScalingBase;

        expNextLevel = levelIncrement;

        // Find possible options
        levelOptions.Clear();

        // If weapon slot free, add non-picked guns
        levelOptions.AddRange(GetNewWeaponOptions());
        // If passive slot free, ad non-picked passives
        levelOptions.AddRange(GetNewItemOptions());
        // Add weapon upgrades
        levelOptions.AddRange(GetWeaponUpgradeOptions());
        // Add passive upgrades
        levelOptions.AddRange(GetItemUpgradeOptions());

        if (OnLevelUp != null) {
            OnLevelUp();
        }

        GetComponent<PlayerStatsHandler>().curHealth = (int)GetComponent<PlayerStatsHandler>().stats[(int)PlayerStats.MaxHP].GetValue();

        if (OnUIChange != null) {
            OnUIChange();
        }
    }

    List<LevelOption> GetNewWeaponOptions() {
        List<LevelOption> options = new List<LevelOption>();

        if (GetComponent<EquipmentHandler>().weapons.Count < GetComponent<PlayerStatsHandler>().stats[(int)PlayerStats.WeaponSlots].GetValue()) {
            foreach(GameObject weapon in EquipmentManager.instance.possibleWeapons) {
                // Create new option
                LevelOption option = new LevelOptionNewWeapon(gameObject, weapon);
                options.Add(option);
            }
        }

        return options;
    }

    List<LevelOption> GetWeaponUpgradeOptions() {
        List<LevelOption> options = new List<LevelOption>();

        foreach(GameObject weapon in GetComponent<EquipmentHandler>().weapons) {
            // Create new option
            LevelOption option = weapon.GetComponent<Weapon>().GetNextLevelOption();
            if (option != null) {
                options.Add(option);
            }
        }

        return options;
    }

    List<LevelOption> GetNewItemOptions() {
        List<LevelOption> options = new List<LevelOption>();

        if (GetComponent<EquipmentHandler>().items.Count < GetComponent<PlayerStatsHandler>().stats[(int)PlayerStats.ItemSlots].GetValue()) {
            foreach(PassiveItem item in EquipmentManager.instance.possibleItems) {
                // Create new option
                LevelOption option = new LevelOptionNewPassive(gameObject, item);
                options.Add(option);
            }
        }

        return options;
    }

    List<LevelOption> GetItemUpgradeOptions() {
        List<LevelOption> options = new List<LevelOption>();

        foreach(PassiveItem item in GetComponent<EquipmentHandler>().items) {
            // Create new option
            LevelOption option = item.GetNextLevelOption();
            if (option != null) {
                options.Add(option);
            }
        }

        return options;
    }
}

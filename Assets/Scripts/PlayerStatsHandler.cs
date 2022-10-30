using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerStatsHandler : Stats
{
    public BaseStats baseStats;

    public string unitName;

    public Stat[] stats;

    public event System.Action OnUIChange;

    void Awake() {
        unitName = baseStats.unitName;

        // Set Sprite
        //gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = baseStats.sprite;

        stats = new Stat[System.Enum.GetNames(typeof(PlayerStats)).Length];

        for (int i = 0; i < stats.Length; i++) {
            stats[i] = new Stat(Regex.Replace(System.Enum.GetName(typeof(PlayerStats), i).ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1"));
            stats[i].SetBaseValue(0);
        }

        // Set stats using BaseUnitStats
        foreach (StatValue sv in baseStats.stats) {
            int slot = (int) sv.stat;
            stats[slot].SetBaseValue(sv.value);
        }

        FullHeal();
    }

    override protected void Start() {
        foreach(GameObject weapon in baseStats.startingWeapons) {
            GetComponent<EquipmentHandler>().AddWeapon(weapon);
        }

        foreach(PassiveItem item in baseStats.startingItems) {
            GetComponent<EquipmentHandler>().AddItem(item);
        }
	}

    public override void TakeDamage(int damage) {
        // STATS
        int reducedDamage = Mathf.RoundToInt(damage * ((100 - stats[(int)PlayerStats.Armour].GetValue()) / 100f));
        if (reducedDamage < 0) {
            reducedDamage = 0;
        }

        base.TakeDamage(reducedDamage);

        if (OnUIChange != null) {
            OnUIChange();
        }
    }

    public void FullHeal() {
        curHealth = (int)stats[(int)PlayerStats.MaxHP].GetValue();
    }
}

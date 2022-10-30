using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerStatsHandler : StatsHandler
{
    public event System.Action OnUIChange;

    override protected void Start() {
        foreach(GameObject weapon in ((PlayerBaseStats)baseStats).startingWeapons) {
            GetComponent<EquipmentHandler>().AddWeapon(weapon);
        }

        foreach(PassiveItem item in ((PlayerBaseStats)baseStats).startingItems) {
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
}

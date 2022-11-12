using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[System.Serializable]
public class LevelOptionUpgradePassive : LevelOption
{
    public PassiveItem item;

	public PlayerStats stat;
	public Modifier modifier;

    public LevelOptionUpgradePassive(GameObject player) : base(player) {
    }

	public override void Select()
	{
		player.GetComponent<PlayerStatsHandler>().stats[(int)stat].AddModifier(modifier);
		item.level++;
	}

	public override string MakeDescription() {
		string description = "Lv. " + item.level + "\n";

		if (modifier.type == ModifierTypes.Flat) {
        	description += "+" + modifier.value + " " + Regex.Replace(stat.ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
		} else if (modifier.type == ModifierTypes.Multiplier) {
			description += "+" + modifier.value + "% " + Regex.Replace(stat.ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
		}

		return description;
    }
}

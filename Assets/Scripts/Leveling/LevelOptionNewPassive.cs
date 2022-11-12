using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LevelOptionNewPassive : LevelOption
{
    PassiveItem item;

    public LevelOptionNewPassive(GameObject player, PassiveItem item) : base(player) {
        this.item = item;

        this.icon = item.sprite;

        this.levelName = item.itemName;
        this.description = MakeDescription();
    }

    public override void Select()
	{
        player.GetComponent<PlayerStatsHandler>().stats[(int)item.stat].AddModifier(item.modifier);

        player.GetComponent<EquipmentHandler>().AddItem(item);
	}

    public override string MakeDescription() {
		if (item.modifier.type == ModifierTypes.Flat) {
        	description += "+" + item.modifier.value + " " + Regex.Replace(item.stat.ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
		} else if (item.modifier.type == ModifierTypes.Multiplier) {
			description += "+" + item.modifier.value + "% " + Regex.Replace(item.stat.ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
		}

		return description;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[System.Serializable]
public class LevelOptionUpgradeWeapon : LevelOption
{
    public Weapon weapon;

	public WeaponStats stat;
	public Modifier modifier;

    public LevelOptionUpgradeWeapon(GameObject player) : base(player) {
    }

	public override void Select()
	{
		weapon.weaponStats[(int)stat].AddModifier(modifier);
		weapon.level++;
	}

	public override string MakeDescription() {
		if (modifier.type == ModifierTypes.Flat) {
        	return description = "+" + modifier.value + " " + Regex.Replace(stat.ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
		} else if (modifier.type == ModifierTypes.Multiplier) {
			return description = "+" + modifier.value + "% " + Regex.Replace(stat.ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
		}

		return base.MakeDescription();
    }
}

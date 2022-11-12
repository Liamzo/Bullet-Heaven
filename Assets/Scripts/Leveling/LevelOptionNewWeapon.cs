using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOptionNewWeapon : LevelOption
{
    GameObject weapon;

    public LevelOptionNewWeapon(GameObject player, GameObject weapon) : base(player) {
        this.weapon = weapon;
        this.icon = weapon.GetComponent<Weapon>().sprite;

        this.levelName = weapon.GetComponent<Weapon>().weaponName;
        this.description = "";
    }

	public override void Select()
	{
		player.GetComponent<EquipmentHandler>().AddWeapon(weapon);
	}
}

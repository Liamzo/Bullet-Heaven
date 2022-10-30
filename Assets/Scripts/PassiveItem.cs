using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[CreateAssetMenu(fileName = "New Passive Item", menuName = "Items/New Item")]
public class PassiveItem: ScriptableObject
{
    public string itemName;

    public Sprite sprite;

    public PlayerStats stat;
	public Modifier modifier;

    public List<LevelOptionUpgradePassive> levelUpgrades;
    public int level = 1;

    public GameObject player;

    public LevelOptionUpgradePassive GetNextLevelOption() {
        if (level <= levelUpgrades.Count) {
            LevelOptionUpgradePassive option = levelUpgrades[level-1];
            option.player = player;
            option.item = this;
            option.description = option.description == "" ? option.MakeDescription() : option.description;
            option.icon = option.icon == null ? sprite : option.icon;
            return option;
        }

        return null;
    }
}

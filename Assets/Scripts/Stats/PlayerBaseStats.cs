using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Block", menuName = "Stats/New Player Stat Block")]
public class PlayerBaseStats : BaseStats {
    public List<GameObject> startingWeapons;
    public List<PassiveItem> startingItems;
}
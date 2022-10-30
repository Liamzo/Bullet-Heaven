using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Block", menuName = "Stats/New Stat Block")]
public class BaseStats : ScriptableObject {
    public string unitName;

    public Sprite sprite;

    public List<StatValue> stats;

    public List<GameObject> startingWeapons;
    public List<PassiveItem> startingItems;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public List<GameObject> UnlockedWeapons;
    public List<GameObject> possibleWeapons;

    public List<PassiveItem> UnlockedItems;
    public List<PassiveItem> possibleItems;

    void Awake()
    {
        instance = this;

        possibleWeapons = UnlockedWeapons;
        possibleItems = UnlockedItems;
    }

    public void RemoveWeapon(GameObject weapon) {
        possibleWeapons.Remove(weapon);
    }

    public void RemoveItem(PassiveItem item) {
        possibleItems.Remove(item);
    }
}

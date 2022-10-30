using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public List<Transform> gunSlots = new List<Transform>();

    public List<PassiveItem> items = new List<PassiveItem>();

    public void AddWeapon(GameObject weapon) {
        if (weapons.Count >= GetComponent<PlayerStatsHandler>().stats[(int)PlayerStats.WeaponSlots].GetValue()) {
            return;
        }

        GameObject go = Instantiate(weapon, gunSlots[weapons.Count]);
        weapons.Add(go);
        EquipmentManager.instance.RemoveWeapon(weapon);
    }

    public void AddItem(PassiveItem item) {
        if (items.Count >= GetComponent<PlayerStatsHandler>().stats[(int)PlayerStats.ItemSlots].GetValue()) {
            return;
        }

        PassiveItem newItem = Instantiate(item);
        newItem.player = gameObject;
        items.Add(newItem);

        EquipmentManager.instance.RemoveItem(item);
    }
}

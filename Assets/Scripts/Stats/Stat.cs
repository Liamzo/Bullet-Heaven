using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    public string name;

    // public enum StatType {
    //     Base,
    //     Multiplier
    // }
    // public StatType statType;

    [SerializeField]
    private float baseValue;

    [SerializeField]
    private List<int> flatModifiers = new List<int>();

    [SerializeField]
    private List<float> percentModifiers = new List<float>();

    public Stat(string name) {
        this.name = name;
    }

    public float GetValue() {
        float finalValue = baseValue;

        flatModifiers.ForEach(x => finalValue += x);

        float multiplier = 100f;

        percentModifiers.ForEach(x => multiplier += x);

        return finalValue * (multiplier / 100f);
    }

    public float GetBaseValue() {
        return baseValue;
    }

    public void SetBaseValue(float value) {
        baseValue = value;
    }

    void AddModifier (int modifier) {
        if (modifier != 0) {
            flatModifiers.Add(modifier);
        }
    }
    void AddModifier (float modifier) {
        if (modifier != 0) {
            percentModifiers.Add(modifier);
        }
    }
    public void AddModifier (Modifier modifier) {
        if (modifier.value != 0) {
            if (modifier.type == ModifierTypes.Flat) {
                AddModifier((int)modifier.value);
            } else if (modifier.type == ModifierTypes.Multiplier) {
                AddModifier((float)modifier.value);
            }
        }
    }

    public void RemoveModifier (int modifier) {
        if (modifier != 0) {
            flatModifiers.Remove(modifier);
        }
    }
    public void RemoveModifier (float modifier) {
        if (modifier != 0) {
            percentModifiers.Remove(modifier);
        }
    }
}

[System.Serializable]
public enum PlayerStats {
    MaxHP,
    AttackSpeed,
    Damage,
    Armour,
    MoveSpeed,
    Range,
    WeaponSlots,
    ItemSlots
}

[System.Serializable]
public enum WeaponStats {
    Damage,
    AttackSpeed,
    Range,
    Projectiles
}

[System.Serializable]
public struct StatValue {
    public PlayerStats stat;
    public int value;
}
[System.Serializable]
public struct WeaponStatValue {
    public WeaponStats stat;
    public int value;
}

[System.Serializable]
public enum ModifierTypes {
    Flat,
    Multiplier
}
[System.Serializable]
public struct Modifier {
    public ModifierTypes type;
    public float value;
}

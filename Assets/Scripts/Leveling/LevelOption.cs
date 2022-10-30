using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LevelOption
{
    public GameObject player;

    public string description;
    public Sprite icon;

    public LevelOption(GameObject player) {
        this.player = player;
    }

    public virtual void Select() {
        Debug.Log("boop");
    }

    public virtual string MakeDescription() {
        return "no desc";
    }
}

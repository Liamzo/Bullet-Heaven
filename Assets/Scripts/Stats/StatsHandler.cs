using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class StatsHandler : MonoBehaviour
{
    public string unitName;
    public BaseStats baseStats;
    public Stat[] stats;
    public int curHealth;

    public Material deadMaterial;

    public System.Action<BaseController> OnDeath = delegate { };

    public float deadTimer = 3f;
    public float deadClock = 0f;
    public bool dead = false;

    protected void Awake() {
        unitName = baseStats.unitName;

        // Set Sprite
        gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = baseStats.sprite;

        stats = new Stat[System.Enum.GetNames(typeof(PlayerStats)).Length];

        for (int i = 0; i < stats.Length; i++) {
            stats[i] = new Stat(Regex.Replace(System.Enum.GetName(typeof(PlayerStats), i).ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1"));
            stats[i].SetBaseValue(0);
        }

        // Set stats using BaseUnitStats
        foreach (StatValue sv in baseStats.stats) {
            int slot = (int) sv.stat;
            stats[slot].SetBaseValue(sv.value);
        }

        FullHeal();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    void Update() {
        if (dead) {
            deadClock += Time.deltaTime;

            if (deadClock >= deadTimer) {
                Destroy(gameObject);
            }
        }
    }

    public virtual void TakeDamage(int damage) {
        curHealth -= damage;

        if (curHealth <= 0f) {
            Die();
        }
    }

    public void Die() {
        BaseController con = GetComponent<BaseController>();
        if (OnDeath != null) {
            OnDeath(con);
        }

        con.sprite.GetComponent<Animator>().enabled = false;
        con.sprite.sortingOrder = -1;
        con.sprite.material = deadMaterial;
        Destroy(con);
        gameObject.layer = LayerMask.NameToLayer("Dead");
        GetComponent<Rigidbody2D>().freezeRotation = false;
        GetComponent<Collider2D>().enabled = false;

        dead = true;
    }

    public void FullHeal() {
        curHealth = (int)stats[(int)PlayerStats.MaxHP].GetValue();
    }
}

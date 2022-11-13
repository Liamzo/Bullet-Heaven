using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;

    public PlayerController playerController;
    public PlayerStatsHandler playerStats;

    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    public List<LevelOptionUpgradeWeapon> levelUpgrades;
    public int level = 1;
    
    public float baseCD;
    public float baseCDTimer;

    public WeaponStatValue[] baseWeaponStats;
    public Stat[] weaponStats;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;


    // Audio
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    protected void PlayRandomAudioClip() {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }


    void Awake() {
        weaponStats = new Stat[System.Enum.GetNames(typeof(WeaponStats)).Length];

        for (int i = 0; i < weaponStats.Length; i++) {
            weaponStats[i] = new Stat(Regex.Replace(System.Enum.GetName(typeof(WeaponStats), i).ToString(), "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1"));
            weaponStats[i].SetBaseValue(0);
        }

        // Set stats using BaseUnitStats
        foreach (WeaponStatValue sv in baseWeaponStats) {
            int slot = (int) sv.stat;
            weaponStats[slot].SetBaseValue(sv.value);
        }
    }

    protected virtual void Start() {
        playerController = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStatsHandler>();

        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    protected abstract void Update();

    protected virtual void AimAtTarget(Transform target) {
        Vector3 aimPos = (target.position - transform.position).normalized * 0.7f;

        transform.localPosition = aimPos;

        LookAtTarget(target);
    }

    protected virtual void LookAtTarget(Transform target) {
        Vector3 dir = target.position - transform.position;
        transform.right = dir;

        if (transform.parent.transform.eulerAngles.z >= 90 && transform.parent.transform.eulerAngles.z <= 270) {
            spriteRenderer.flipY = true;
        } else {
            spriteRenderer.flipY = false;
        }
    }

    protected virtual Transform GetClosestEnemy() {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        
        List<Transform> enemyTransforms = new List<Transform>();

        foreach (EnemyController e in enemies) {
            enemyTransforms.Add(e.transform);
        }

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.parent.transform.position;
        foreach (Transform t in enemyTransforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist && dist <= weaponStats[(int)WeaponStats.Range].GetValue() * (1 + (playerStats.stats[(int)PlayerStats.Range].GetValue() / 100f)))
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


    public abstract void Fire();

    public int CalculateDamage() {
        return Mathf.RoundToInt(weaponStats[(int)WeaponStats.Damage].GetValue() * (1 + (playerStats.stats[(int)PlayerStats.Damage].GetValue() / 100f)));
    }

    public float CalculateCD() {
        return baseCD / (1 + (weaponStats[(int)WeaponStats.AttackSpeed].GetValue() / 100f)) / (1 + (playerStats.stats[(int)PlayerStats.AttackSpeed].GetValue() / 100f));
    }

    public float CalculateRange() {
        return weaponStats[(int)WeaponStats.Range].GetValue() * (1 + (playerStats.stats[(int)PlayerStats.Range].GetValue() / 100f));
    }


    public LevelOptionUpgradeWeapon GetNextLevelOption() {
        if (level <= levelUpgrades.Count) {
            LevelOptionUpgradeWeapon option = levelUpgrades[level-1];
            option.player = playerStats.gameObject;
            option.weapon = this;
            option.levelName = weaponName;
            option.description = option.description == "" ? option.MakeDescription() : option.description;
            option.icon = option.icon == null ? sprite : option.icon;
            return option;
        }

        return null;
    }
}

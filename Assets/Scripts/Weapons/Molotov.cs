using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : Weapon
{
    public float duration;
    public float tickTime;

    public float travelTime;
    
    public float size;

    // Update is called once per frame
    protected override void Update() {
        baseCDTimer -= Time.deltaTime;

        Transform target = GetRandomEnemy();

        if (target == null) {
            spriteRenderer.enabled = false;
            return;
        }

        if (CalculateCD() < 1f) {
            spriteRenderer.enabled = true;
        } else if (baseCDTimer <= 0.2f) {
            spriteRenderer.enabled = true;
        }

        if (baseCDTimer <= 0f) {
            Fire();
            baseCDTimer = CalculateCD();
            spriteRenderer.enabled = false;
        }
    }


    public override void Fire() {
        Transform target = GetRandomEnemy();

        if (target == null) {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.layer = LayerMask.NameToLayer("PlayerBullet");
        // STATS
        bullet.GetComponent<MolotovBullet>().target = target.position;
        bullet.GetComponent<MolotovBullet>().damage = CalculateDamage();
        bullet.GetComponent<MolotovBullet>().duration = duration;
        bullet.GetComponent<MolotovBullet>().tickTime = tickTime;
        bullet.GetComponent<MolotovBullet>().travelTime = travelTime;
        bullet.GetComponent<MolotovBullet>().size = size;

        PlayRandomAudioClip();
    }

    protected Transform GetRandomEnemy() {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        
        List<Transform> enemyTransforms = new List<Transform>();

        foreach (EnemyController e in enemies) {
            float dist = Vector3.Distance(e.transform.position, transform.position);

            if (dist <= CalculateRange()) {
                enemyTransforms.Add(e.transform);
            }
        }

        if (enemyTransforms.Count == 0) {
            return null;
        }

        return enemyTransforms[Random.Range(0, enemyTransforms.Count)];
    }
}

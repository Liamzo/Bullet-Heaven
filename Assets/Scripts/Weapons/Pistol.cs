using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    // Update is called once per frame
    protected override void Update() {
        baseCDTimer -= Time.deltaTime;

        // Find and look at target
        Vector3? target = GetClosestEnemy();

        if (target == null) {
            spriteRenderer.enabled = false;
            return;
        }

        AimAtTarget(target.Value);

        if (CalculateCD() < 1f) {
            spriteRenderer.enabled = true;
        } else if (baseCDTimer <= 0.2f) {
            spriteRenderer.enabled = true;
        } else if (baseCDTimer <= CalculateCD() - 0.2f) {
            spriteRenderer.enabled = false;
        }
        
        if (baseCDTimer <= 0f) {
            Fire();
            baseCDTimer = CalculateCD();
        }
    }

    protected override void LookAtTarget(Vector3 target) {
        Vector3 dir = target - transform.position;
        transform.right = dir;

        if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270) {
            spriteRenderer.flipY = true;
        } else {
            spriteRenderer.flipY = false;
        }
    }

    protected override Vector3? GetClosestEnemy() {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        
        List<Transform> enemyTransforms = new List<Transform>();

        foreach (EnemyController e in enemies) {
            enemyTransforms.Add(e.transform);
        }

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemyTransforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist && dist <= CalculateRange())
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin != null ? tMin.position : null;
    }


    public override void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.layer = LayerMask.NameToLayer("PlayerBullet");
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
        // STATS
        bullet.GetComponent<Bullet>().damage = CalculateDamage();
        
        PlayRandomAudioClip();
    }


}

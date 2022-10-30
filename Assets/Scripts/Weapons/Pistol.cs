using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    // Update is called once per frame
    protected override void Update() {
        baseCDTimer -= Time.deltaTime;

        // Find and look at target
        Transform target = GetClosestEnemy();

        if (target == null) {
            return;
        }

        AimAtTarget(target);
        
        if (baseCDTimer <= 0f) {
            Fire();
            baseCDTimer = CalculateCD();
        }
    }

    protected override void AimAtTarget(Transform target) {
        Vector3 dir = target.position - transform.position;
        transform.right = dir;

        if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270) {
            spriteRenderer.flipY = true;
        } else {
            spriteRenderer.flipY = false;
        }
    }

    protected override Transform GetClosestEnemy() {
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
        return tMin;
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

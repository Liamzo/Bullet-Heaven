using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public ParticleSystem fireEffect;
    ParticleSystem.EmissionModule fireEffectEmission;

    public float angle;


    protected override void Start()
    {
        base.Start();
        
        fireEffectEmission = fireEffect.emission;
    }


    // Update is called once per frame
    protected override void Update() {
        baseCDTimer -= Time.deltaTime;

        // Find and look at target
        Vector3 target = playerController.transform.position + playerController.dirForward;

        if (target == null) {
            spriteRenderer.enabled = false;
            return;
        }

        AimAtTarget(target);

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

    public override void Fire() {
        List<StatsHandler> targets = EnemiesInCone();

        foreach(StatsHandler target in targets) {
            target.TakeDamage(CalculateDamage());

            Vector2 dir = (target.transform.position - transform.position).normalized;

            target.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
        }

        PlayRandomAudioClip();
        fireEffect.Play();
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

    List<StatsHandler> EnemiesInCone() {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        
        List<StatsHandler> enemyStats = new List<StatsHandler>();

        foreach (EnemyController e in enemies) {
            float dist = Vector3.Distance(e.transform.position, transform.position);

            Vector2 dir = (e.transform.position - transform.position).normalized;

            if (dist <= CalculateRange() && (Vector3.Angle(firePoint.right, dir) <= angle / 2f || dist < 0.5f)) {
                enemyStats.Add(e.GetComponent<StatsHandler>());
            }
        }

        return enemyStats;
    }
}

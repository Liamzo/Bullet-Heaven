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

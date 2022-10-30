using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public float impactForce;

    float lifeTimer;

    void Update() {
        lifeTimer += Time.deltaTime;

        if (lifeTimer > 2f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.TryGetComponent<StatsHandler>(out StatsHandler stats)) {
            stats.TakeDamage(damage);

            Vector2 dir = (other.transform.position - transform.position).normalized;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            other.collider.GetComponent<Rigidbody2D>().AddForce(rb.velocity.normalized * impactForce, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    public Material deadMaterial;

    public System.Action<BaseController> OnDeath = delegate { };

    public float deadTimer = 3f;
    public float deadClock = 0f;
    public bool dead = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (maxHealth <= 0) {
            maxHealth = 10;
        }
        curHealth = maxHealth;
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
}

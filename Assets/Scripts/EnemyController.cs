using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    public PlayerController player;

    public float moveSpeed;
    public Vector2 movement;

    public float attackCD;
    public float attackCDTimer;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (GameManager.instance.paused == true || player == null) {
            return;
        }
        
        attackCDTimer -= Time.deltaTime;

        // Find vector to player
        movement = (Vector2)player.transform.position - rb.position;

        if (movement.x < 0) {
            sprite.flipX = true;
        } else if (movement.x > 0) {
            sprite.flipX = false;
        }


        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (attackCDTimer <= 0f && other.transform.tag == "Player") {
            attackCDTimer = attackCD;

            other.transform.GetComponent<PlayerStatsHandler>().TakeDamage(damage);
        }
    }
}

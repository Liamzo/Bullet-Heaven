using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public PlayerStatsHandler playerStats;

    public float moveSpeed;
    public Vector2 movement;

    public ParticleSystem footSteps;
    ParticleSystem.EmissionModule footEmission;


    // Start is called before the first frame update
    void Start()
    {
        footEmission = footSteps.emission;

        audioSource.clip = audioClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused == true) {
            return;
        }

        MoveControls();
    }

    void FixedUpdate() {
        if (GameManager.instance.paused == true) {
            return;
        }
        
        if (movement.x < 0) {
            sprite.flipX = true;
        } else if (movement.x > 0) {
            sprite.flipX = false;
        }

        if (movement == Vector2.zero) {
            footEmission.rateOverTime = 0f;

            audioSource.Stop();
        } else {
            footEmission.rateOverTime = 25f;

            if (audioSource.isPlaying == false) {
                audioSource.Play();
            }
        }

        // STATS
        float adjustedMoveSpeed = moveSpeed * (1 + (playerStats.stats[(int)PlayerStats.MoveSpeed].GetValue() / 100f));

        rb.MovePosition(rb.position + movement.normalized * adjustedMoveSpeed * Time.fixedDeltaTime);
    }

    public void MoveControls() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
}

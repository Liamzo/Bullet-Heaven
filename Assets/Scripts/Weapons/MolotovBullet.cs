using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovBullet : MonoBehaviour
{
    public Vector2 target;

    public int damage;

    public float duration;
    public float tickTime;

    public float travelTime;
    
    public float size;

    public SpriteRenderer spriteRenderer;
    public Collider2D area;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    Vector2 startPosition;
    public Sprite fireSprite;
    float travelled;
    bool exploded;
    float durationTimer;

    List<HitEnemy> hitEnemies = new List<HitEnemy>();


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (exploded == false) {
            travelled += Time.deltaTime / travelTime;

            gameObject.transform.position = Vector2.Lerp(startPosition, target, travelled);

            if (travelled >= 1f) {
                exploded = true;

                spriteRenderer.sprite = fireSprite;
                area.enabled = true;

                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            }   
        } else {
            durationTimer += Time.deltaTime;

            if (durationTimer >= duration) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.TryGetComponent<Stats>(out Stats stats)) {
            int index = hitEnemies.FindIndex(x => x.enemy == other.gameObject);

            if (index >= 0) {
                if (durationTimer >= hitEnemies[index].hitTime + tickTime) {
                    hitEnemies.RemoveAt(index);
                }
            } else {
                hitEnemies.Add(new HitEnemy(other.gameObject, durationTimer));

                stats.TakeDamage(damage);
            }
        }
    }
}

public struct HitEnemy {
    public GameObject enemy;
    public float hitTime;

    public HitEnemy(GameObject e, float time) {
        this.enemy = e;
        this.hitTime = time;
    }
}

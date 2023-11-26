using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D rb;
    public int damageAmount = 20;
    public Balloon balloon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(100f, 200f), moveSpeed * 25f));
    }

    void Update()
    {
        if (balloon != null && balloon.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger is with an object tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Get the PlayerControl component from the player object
            PlayerControl playerControl = other.GetComponent<PlayerControl>();

            // Check if the playerControl component is not null
            if (playerControl != null)
            {
                playerControl.TakeDamage(damageAmount);
            }
        }
    }
}
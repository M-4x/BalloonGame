using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;
    Vector2 moveDirection;
    Vector2 mousePosition;
    public int maxHealth = 100;
    private int currentHealth;
    public PlayerHealthBar playerHealthBar;
    public GameObject deathMenu;
    public float shotDelay = 0.2f;
    private float timeSinceLastShot;
    public AudioClip shootingSound;
    public AudioClip hitSound;
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = shootingSound;
        audioSource2.clip = hitSound;
        audioSource1.volume = 0.5f;
        audioSource2.volume = 1.0f;

        Time.timeScale = 1;
        deathMenu.SetActive(false);
        currentHealth = maxHealth;
        if (playerHealthBar != null)
        {
            playerHealthBar.SetMaxHealth(maxHealth);
            playerHealthBar.SetHealth(currentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0) && Time.time - timeSinceLastShot > shotDelay)
        {
            weapon.Fire();
            timeSinceLastShot = Time.time;
            audioSource1.PlayOneShot(shootingSound);
        }

        if (Input.GetMouseButtonDown(1))
        {
            currentHealth = 1000;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        // Calculate the movement without affecting the aim direction
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // Calculate the aim direction based on the mouse position
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar
        if (playerHealthBar != null)
        {
            playerHealthBar.SetHealth(currentHealth);
            audioSource2.PlayOneShot(hitSound);
        }

        // Example: Check for player death
        if (currentHealth <= 0)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

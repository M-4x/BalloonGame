using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int maxHealth = 100;
    public int currentHealth;
    private Rigidbody2D rb;
    private HealthBar healthBar;
    private float startTime;
    private bool balloonPopped = false;
    public TextMeshProUGUI timerText;
    public ScoreManager scoreManager;
    public GameObject victoryMenu;
    public ParticleSystem explosionParticles;
    public float delayBeforeVictoryMenu = 2.0f;
    private bool stopTimer = false;
    public AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(true);
        Time.timeScale = 1;
        victoryMenu.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<HealthBar>(); //
        currentHealth = maxHealth;
        startTime = Time.time;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        StartRandomMovement();
    }

    void Update()
    {
        if (!stopTimer)
        {
            ShowTimer();
        }
    }

    void StartRandomMovement()
    {
        StartCoroutine(RandomMovement());
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.velocity = randomDirection * moveSpeed;

            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f)); // Adjust the time between movements
        }
    }

    public void TakeDamage(int damage)
    {
        // Handle balloon hit
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // Increase balloon size with each pin hit (you may need to adjust the scale factor)
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);

        if (currentHealth <= 0 && !balloonPopped)
        {
            audioSource.PlayOneShot(explosionSound);
            explosionParticles.transform.position = transform.position;
            explosionParticles.Play();
            balloonPopped = true;
            StopCoroutine(RandomMovement());
            ShowTimer();

            float elapsedTime = Time.time - startTime;
            Score newScore = new Score(elapsedTime);
            scoreManager.AddScore(newScore);
            StartCoroutine(ShowVictoryMenuWithDelay());

            stopTimer = true;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    void ShowTimer()
    {
        float endTime = Time.time;
        float elapsedTime = endTime - startTime;

        // Display the elapsed time using TextMeshPro
        if (timerText != null)
        {
            timerText.text = "Time: " + elapsedTime.ToString("F2") + "s";
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 oppositeDirection = -rb.velocity.normalized;
        rb.velocity = oppositeDirection * moveSpeed;
    }

    IEnumerator ShowVictoryMenuWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeVictoryMenu);
        Time.timeScale = 0;
        victoryMenu.SetActive(true);
    }
}

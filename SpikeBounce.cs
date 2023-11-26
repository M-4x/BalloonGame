using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;
    public AudioClip wallHit;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D other) {
        audioSource.PlayOneShot(wallHit);
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);
    }
}

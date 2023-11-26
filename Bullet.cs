using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 100;
    public GameObject impactEffect;
    public float impactEffectDuration = 1.0f;
    public AudioClip hitSound;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SFXManager.instance.GetVolume() * 100f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balloon"))
        {
            Balloon balloon = collision.gameObject.GetComponent<Balloon>();
            if (balloon != null)
            {
                audioSource.PlayOneShot(hitSound);
                balloon.TakeDamage(damage);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, impactEffectDuration);
            }
            GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(impact, impactEffectDuration);
        }else{
            Destroy(gameObject);
        }
    }
}


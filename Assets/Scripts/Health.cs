using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitParticles;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            audioManager.PlayDamageSFX();

            TakeDamage(damageDealer.GetDamage());
            PlayHitPaticles();
            damageDealer.Hit(); 

            if (applyCameraShake)
            {

                cameraShake.Play();
            
            }
        }
    }

    void TakeDamage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void PlayHitPaticles()
    {
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }
}

using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float basefiringRate = 0.2f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] AudioSource playerShootAudio;

    [Header("AI Variables")]
    [SerializeField] bool useAI = true;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] float fireingRateVariance = 0f;

    [HideInInspector] public bool isFiring;
    Coroutine fireCoroutine;
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (useAI)
        {
            isFiring = true;
        }
    }


    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring)
        {
            if (fireCoroutine == null)
            {
                fireCoroutine = StartCoroutine(FireContinuously());
            }
        }
        else
        {
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
        }
    }

    IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            GameObject Projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = transform.up * projectileSpeed;

            playerShootAudio.Play();
            Destroy(Projectile, projectileLifetime);

            float waitTime = Random.Range(basefiringRate - fireingRateVariance, basefiringRate + fireingRateVariance);

            waitTime = Mathf.Clamp(waitTime, minimumFiringRate, float.MaxValue);

            audioManager.PlayShootingSFX();

            yield return new WaitForSeconds(waitTime);
        }

        fireCoroutine = null;
    }
}

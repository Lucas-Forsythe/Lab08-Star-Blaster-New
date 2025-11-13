using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] float projectileLifetime = 5f;

    public bool isFiring;

    Coroutine fireCoroutine;

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
        while (true)
        {
            GameObject Projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D projectileRB = Projectile.GetComponent<Rigidbody2D>();
            projectileRB.linearVelocityY = projectileSpeed;

            Destroy(Projectile, projectileLifetime);

            yield return new WaitForSeconds(firingRate);
        }
    }
}

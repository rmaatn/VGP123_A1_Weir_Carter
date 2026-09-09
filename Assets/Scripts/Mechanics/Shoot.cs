using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Shoot : MonoBehaviour 
{ 
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField, Tooltip("Initial velocity of the projectile when fired - this assumes the projectile is facing right")] private Vector2 initialShotVelocity = new Vector2(3, 3);
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Projectile projectilePrefab;

    private Vector2 leftShotVelocity;

    public Action OnShotFired;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (initialShotVelocity == Vector2.zero)
        {
            initialShotVelocity = new Vector2(3, 7.5f);
            Debug.LogWarning("Shoot: Initial shot velocity was not set, defaulting to (3, 7.5)");
        }

        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            Debug.LogError("Shoot: Spawn points and projectile prefab must be assigned in the inspector on " + gameObject.name);
        }

        leftShotVelocity = new Vector2(-initialShotVelocity.x, initialShotVelocity.y);
    }

    public void Fire()
    {
        Projectile curProjectile;
        float inheritedXVelocity = rb.linearVelocity.x;

        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.SetVelocity(new Vector2(initialShotVelocity.x + inheritedXVelocity, initialShotVelocity.y));
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curProjectile.SetVelocity(new Vector2(leftShotVelocity.x + inheritedXVelocity, leftShotVelocity.y));
        }

        OnShotFired?.Invoke();
    }
}

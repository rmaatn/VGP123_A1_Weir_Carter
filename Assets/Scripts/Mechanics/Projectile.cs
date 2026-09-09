using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType = ProjectileType.PlayerProjectile;
    [SerializeField, Range(0.5f, 10f)] private float lifetime = 10f;
    [SerializeField] private int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return;
        }

        Destroy(gameObject);
    }
}

public enum ProjectileType
{
    PlayerProjectile,
    EnemyProjectile
}
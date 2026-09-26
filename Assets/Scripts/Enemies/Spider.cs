using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Spider : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRange = 6f;
    [SerializeField, Range(0f, 180f)] private float detectionAngle = 60f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack")]
    [SerializeField] private Projectile webPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float webSpeed = 8f;
    [SerializeField] private float fireCooldown = 1.5f;

    [Header("Audio")]
    [SerializeField] private AudioClip webShootSound;
    [SerializeField, Range(0f, 1f)] private float shootVolume = 0.3f;

    private float cooldownTimer;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0f)
            return;

        Collider2D player = FindPlayerInCone();

        if (player == null)
            return;

        FireWeb(player.transform);

        cooldownTimer = fireCooldown;
    }

    private Collider2D FindPlayerInCone()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(
            transform.position,
            detectionRange,
            playerLayer
        );

        foreach (Collider2D player in players)
        {
            Vector2 directionToPlayer =
                player.transform.position - transform.position;

            float angle = Vector2.Angle(
                Vector2.down,
                directionToPlayer
            );

            if (angle <= detectionAngle / 2f)
            {
                return player;
            }
        }

        return null;
    }

    private void FireWeb(Transform target)
    {
        Vector2 direction =
            (target.position - firePoint.position).normalized;

        Projectile web = Instantiate(
            webPrefab,
            firePoint.position,
            Quaternion.identity
        );

        web.SetVelocity(direction * webSpeed);

        audioSource.PlayOneShot(webShootSound, shootVolume);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );

        Vector3 leftDirection =
            Quaternion.Euler(
                0f,
                0f,
                detectionAngle / 2f
            ) * Vector3.down;

        Vector3 rightDirection =
            Quaternion.Euler(
                0f,
                0f,
                -detectionAngle / 2f
            ) * Vector3.down;

        Gizmos.DrawRay(
            transform.position,
            leftDirection * detectionRange
        );

        Gizmos.DrawRay(
            transform.position,
            rightDirection * detectionRange
        );

        Gizmos.DrawRay(
            transform.position,
            Vector3.down * detectionRange
        );
    }
}
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CentipedeEnemy : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private float startPosX;
    [SerializeField] private float endPosX;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Damage")]
    [SerializeField] private float damageCooldown = 1f;

    private SpriteRenderer spriteRenderer;
    private int moveDirection = 1;
    private int lastDirection = 0;
    private float damageTimer = 0f;
    private bool playerInRange = false;
    private GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        Patrol();
        SpriteFlip();

        if (playerInRange)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0f)
            {
                DealDamage();
                damageTimer = damageCooldown;
            }
        }
    }

    private void Patrol()
    {
        float targetX = moveDirection == -1 ? endPosX : startPosX;
        Vector3 target = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Mathf.Approximately(transform.position.x, targetX))
        {
            moveDirection *= -1;
        }
    }

    private void SpriteFlip()
    {
        if (moveDirection == lastDirection) return;

        spriteRenderer.flipX = moveDirection == -1;
        lastDirection = moveDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            damageTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void DealDamage()
    {
        if (gameManager != null)
        {
            gameManager.Lives -= 1;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 start = new Vector3(startPosX, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(endPosX, transform.position.y, transform.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(start, 0.1f);
        Gizmos.DrawSphere(end, 0.1f);
    }
}
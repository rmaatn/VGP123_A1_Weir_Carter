using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField, Range(0f, 1f)] private float damageVolume = 0.3f;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator anim;
    private AudioSource audioSource;

    private Vector2 groundCheckPos => CalculateGroundCheckPos();

    private Vector2 GroundCheckSize()
    {
        float width = col.bounds.size.x * 0.95f;
        float height = 0.1f;
        return new Vector2(width, height);
    }

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = col.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }
    private bool _isGrounded;

    private int previousLives;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (GameManager.Instance != null)
        {
            previousLives = GameManager.Instance.Lives;
            GameManager.Instance.OnLivesChanged += HandleLivesChanged;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged -= HandleLivesChanged;
        }
    }

    private void HandleLivesChanged(int newLives)
    {
        if (newLives < previousLives)
        {
            audioSource.PlayOneShot(damageSound, damageVolume);
        }

        previousLives = newLives;
    }

    void Update()
    {
        _isGrounded = Physics2D.OverlapBox(groundCheckPos, GroundCheckSize(), 0, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool jumpAttackInput = Input.GetButtonDown("Jump") && !_isGrounded && Input.GetAxis("Vertical") > 0;

        if (horizontalInput != 0) SpriteFlip(horizontalInput);

        rb.linearVelocityX = horizontalInput * moveSpeed;

        if (jumpInput && _isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack") && !_isGrounded)
        {
            rb.linearVelocityX = 0f;
            rb.linearVelocityY = -15f;
        }

        anim.SetBool("isGrounded", _isGrounded);
        anim.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocityX));
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    }

    void SpriteFlip(float horizontalInput) => sr.flipX = (horizontalInput < 0);
}
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.125f;
    [SerializeField] private float speed = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip collectSound;
    [SerializeField, Range(0f, 1f)] private float collectVolume = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, collectVolume);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.CollectMarble();
            }

            Destroy(gameObject);
        }
    }

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float sineWave = Mathf.Sin(Time.time * speed) * amplitude;

        transform.position = new Vector3(startPosition.x, startPosition.y + sineWave, startPosition.z);
    }
}
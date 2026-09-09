using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.125f;
    [SerializeField] private float speed = 1f;
   
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private Vector3 startPosition;

    void Start()
    {
        // Store the original starting position
        startPosition = transform.position;
    }

    void Update()
    {
        float sineWave = Mathf.Sin(Time.time * speed) * amplitude;

        transform.position = new Vector3(startPosition.x, startPosition.y + sineWave, startPosition.z);
    }
}
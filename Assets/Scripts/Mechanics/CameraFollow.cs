using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private Transform target;
   
    
    void Start()
    {
        
    }

    void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.LogError("CameraFollow: No target assigned and no GameObject with tag 'Player' founr in the scene.");
                return;
            }   

            target = player.transform;
        }
        
        if (target == null) return;

        Vector3 currentPos = transform.position;

        currentPos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);

        transform.position = currentPos;
    }
}

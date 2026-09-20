using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    void Start() => GameManager.Instance.SpawnPlayer(transform.position);
}

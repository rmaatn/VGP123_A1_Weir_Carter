using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject marblePrefab;
    [SerializeField] private int numberToSpawn = 5;
    [SerializeField] private Transform spawnArea;

    void Start()
    {

        Animator anim = marblePrefab.GetComponent<Animator>();
        int numberOfStyles = anim.runtimeAnimatorController.animationClips.Length;

        int chosenStyle = Random.Range(1, numberOfStyles + 1);

        // Spawn the marbles
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(
                    spawnArea.position.x - spawnArea.localScale.x / 2,
                    spawnArea.position.x + spawnArea.localScale.x / 2
                ),
                Random.Range(
                    spawnArea.position.y - spawnArea.localScale.y / 2,
                    spawnArea.position.y + spawnArea.localScale.y / 2
                ),
                0
            );

            // Create the marble
            GameObject marble = Instantiate(
                marblePrefab,
                spawnPosition,
                Quaternion.identity
            );

            // Give it the chosen style
            marble.GetComponent<Marble>().Style = chosenStyle;
        }
    }
}

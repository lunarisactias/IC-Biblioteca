using System.Collections;
using UnityEngine;

public class SpawnItens : MonoBehaviour
{
    [SerializeField] private GameObject[] itensToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            int randomItem = Random.Range(0, itensToSpawn.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(itensToSpawn[randomItem], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }

}


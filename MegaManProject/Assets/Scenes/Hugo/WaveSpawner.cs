using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] float spawninterval = 10f;
    [SerializeField] private float spawnincreaserate = 1.2f;
    [SerializeField] private int enemiesperwave = 1;
    [SerializeField] private float nextwave;

    private void Start()
    {
        nextwave = Time.time + spawninterval;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            if (Time.time >= nextwave)
            {
                StartCoroutine(SpawnEnemies());
                nextwave = Time.time + spawninterval;
                enemiesperwave = (int)(enemiesperwave * spawnincreaserate) + 1;
            }
            yield return null;
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesperwave; i++)
        {
            Transform spawnpoint = spawnpoints[i % spawnpoints.Length];
            Instantiate(enemy, spawnpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }


    }
}

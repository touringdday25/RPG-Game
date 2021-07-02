using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]GameObject enemyToSpawn;
    private float timeBetweenSpawns = 0;
    [SerializeField][Tooltip("Spawn rate is in seconds")]private float spawnRate;
    private int currentSpawned = 0;
    [SerializeField][Tooltip("Max spawns needs to be less than avalible spawn points.")]private int maxSpawns;

    [SerializeField]private List<GameObject> spawners;

    [SerializeField]private bool[] spawnUsed;

    //Character player;


    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            spawners.Clear();
            foreach (Transform transform in GetComponentsInChildren<Transform>())//Takes all transforms in children to make spawn points.
            {
                if(transform != this.transform)
                {
                    spawners.Add(transform.gameObject);

                }
            }
            spawnUsed = new bool[spawners.Count];//Used to keep Spawners from having multiple enemies on the same spawn point.

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpawned < maxSpawns)
        {
            timeBetweenSpawns += Time.deltaTime;

        }
        if(timeBetweenSpawns >= spawnRate && currentSpawned < maxSpawns)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int rdm = Random.Range(0, spawners.Count);
        if (!spawnUsed[rdm])
        {
            var curEnemy = Instantiate(enemyToSpawn, spawners[rdm].transform.position, spawners[rdm].transform.rotation, spawners[rdm].transform);
            curEnemy.GetComponent<EnemyHealth>().Spawner = this;
            curEnemy.GetComponent<EnemyHealth>().SpawnerNum = rdm;
            timeBetweenSpawns = 0;
            currentSpawned++;
            spawnUsed[rdm] = true;


        }
        else
        {
            SpawnEnemy();
        }
    }

    public void RemoveEnemy(EnemyHealth enemy)
    {
        currentSpawned--;
        spawnUsed[enemy.SpawnerNum] = false;
    }


}

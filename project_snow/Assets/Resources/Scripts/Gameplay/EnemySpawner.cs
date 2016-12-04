using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    //public Transform enemySpawnPoint;

    // private List<GameObject> enemies;

    public int enemyCount = 10;

    public float MinX = -20;
    public float MaxX = 30;
    public float MinZ = 30;
    public float MaxZ = 100;
    public float y = 0.7f;

    public void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject currEnemy = createEnemy();
            NetworkServer.Spawn(currEnemy);
        }

        Debug.Log("SpawnEnemies() executed");
    }

    private GameObject createEnemy()
    {
        float x = Random.Range(MinX, MaxX);
        float z = Random.Range(MinZ, MaxZ);

        GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
        return enemy;
    }
}

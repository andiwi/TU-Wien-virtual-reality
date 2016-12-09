using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
	public GameObject enemiesContainer;
    public GameObject enemyPrefab;
    //public Transform enemySpawnPoint;

	private GameManager gameManager;

    // private List<GameObject> enemies;

    public int enemyCount = 10;

    public float MinX = -20;
    public float MaxX = 30;
    public float MinZ = 30;
    public float MaxZ = 100;
    public float y = 0.7f;

    public void Start()
    {
		if (isServer) {
			gameManager = this.GetComponent<GameManager> ();
			SpawnEnemies ();
		}
    }
		
    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject currEnemy = createEnemy();
            NetworkServer.Spawn(currEnemy);
        }

        //Debug.Log("SpawnEnemies() executed");
    }
		
    private GameObject createEnemy()
    {
        float x = Random.Range(MinX, MaxX);
        float z = Random.Range(MinZ, MaxZ);

        GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
		enemy.transform.parent = enemiesContainer.transform;

		gameManager.IncreaseEnemiesAlive ();
        return enemy;
    }

	public void RemoveEnemy(GameObject enemy) {
		if(isServer) {
			NetworkServer.Destroy (enemy);
			gameManager.DecreaseEnemiesAlive ();
		}
	}
}

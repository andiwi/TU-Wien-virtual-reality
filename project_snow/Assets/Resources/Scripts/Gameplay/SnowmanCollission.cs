using UnityEngine;
using System.Collections;

public class SnowmanCollission : MonoBehaviour {

	private GameObject gameManager;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "playerSnowball") {
			Debug.Log ("hit by player snowball");
			EnemySpawner enemySpawner = gameManager.GetComponent<EnemySpawner> ();
			enemySpawner.RemoveEnemy (this.gameObject);
		}
	}
}

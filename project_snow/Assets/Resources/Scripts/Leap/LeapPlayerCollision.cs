using UnityEngine;
using System.Collections;

public class LeapPlayerCollision : MonoBehaviour {

	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		GameObject gameManagerObj = GameObject.Find ("GameManager");
		gameManager = gameManagerObj.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "enemySnowballActivated") {
			gameManager.HitByEnemySnowball ();
		}
	}
}

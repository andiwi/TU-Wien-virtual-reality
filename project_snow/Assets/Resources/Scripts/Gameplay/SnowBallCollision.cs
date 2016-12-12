using UnityEngine;
using System.Collections;

public class SnowBallCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter Snowball / switching tag");
        gameObject.tag = "enemySnowballDeactivated";
    }
}

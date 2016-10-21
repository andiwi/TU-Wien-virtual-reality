using UnityEngine;
using System.Collections;

public class HoleTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		other.isTrigger = true;
	}

	void OnTriggerStay (Collider other) {
		//Debug.Log ("Object is within the trigger.");
	}

	void OnTriggerExit (Collider other) {
		other.isTrigger = false; //so that ball cannot fall back in box
		other.attachedRigidbody.useGravity = true;
	}
}

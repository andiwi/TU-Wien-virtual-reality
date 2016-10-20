using UnityEngine;
using System.Collections;

public class HoleTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object entered the trigger.");
	}

	void OnTriggerStay (Collider other) {
		Debug.Log ("Object is within the trigger.");
	}

	void OnTriggerExit (Collider other) {
		Debug.Log ("Object exited the trigger.");
	}
}

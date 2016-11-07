﻿using UnityEngine;
using System.Collections;

// TODO: this script CAN be used to detect the events of a right networked hand touching a shared object
// fill in the implementation and communicate touching events to either LeapGrab and ViveGrab by setting the rightHandTouching variable
// ALTERNATIVELY, implement the verification of the grabbing conditions in a way  your prefer
// TO REMEMBER: only the localPlayer (networked hands belonging to the localPlayer) should be able to "touch" shared objects

public class TouchRight : MonoBehaviour {

    // the implementation of a touch condition might be different for Vive and Leap 
    public bool vive;
    public bool leap;

	void OnTriggerEnter(Collider other)
	{
		GameObject playerController = GameObject.Find ("PlayerController");
		AuthorityManager am = other.gameObject.Find ("AuthorityManager");

		if (vive) {
			ViveGrab viveGrab = playerController.GetComponent<ViveGrab> ();
			viveGrab.SetRightHandTouching(true);
			viveGrab.SetAuthorityManagerRight (am);
		} else if(leap)
		{
			LeapGrab leapGrab = playerController.GetComponent<LeapGrab> ();
			leapGrab.SetRightHandTouching(true);
			leapGrab.SetAuthorityManagerRight (am);
		}
	}

	void OnTriggerExit()
	{
		GameObject playerController = GameObject.Find ("PlayerController");

		if (vive) {
			ViveGrab viveGrab = playerController.GetComponent<ViveGrab> ();
			viveGrab.SetRightHandTouching(false);
			viveGrab.SetAuthorityManagerRight (null);

		} else if(leap)
		{
			LeapGrab leapGrab = playerController.GetComponent<LeapGrab> ();
			leapGrab.SetRightHandTouching(false);
			leapGrab.SetAuthorityManagerRight (null);
		}
	}
}

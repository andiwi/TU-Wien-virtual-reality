using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// TODO: this script CAN be used to detect the events of a left networked hand touching a shared object
// fill in the implementation and communicate touching events to either LeapGrab and ViveGrab by setting the leftHandTouching variable
// ALTERNATIVELY, implement the verification of the grabbing conditions in a way  your prefer
// TO REMEMBER: only the localPlayer (networked hands belonging to the localPlayer) should be able to "touch" shared objects

public class TouchLeft : MonoBehaviour {

    public bool vive;
    public bool leap;

	void OnTriggerEnter()
	{
		GameObject playerController = GameObject.Find ("PlayerController");

		if (vive) {
			ViveGrab viveGrab = playerController.GetComponent<ViveGrab> ();
			viveGrab.SetLeftHandTouching(true);
		} else if(leap)
		{
			LeapGrab leapGrab = playerController.GetComponent<LeapGrab> ();
			leapGrab.SetLeftHandTouching(true);
		}
	}

	void OnTriggerExit()
	{
		GameObject playerController = GameObject.Find ("PlayerController");

		if (vive) {
			ViveGrab viveGrab = playerController.GetComponent<ViveGrab> ();
			viveGrab.SetLeftHandTouching(true);
		} else if(leap)
		{
			LeapGrab leapGrab = playerController.GetComponent<LeapGrab> ();
			leapGrab.SetLeftHandTouching(false);
		}
	}

}

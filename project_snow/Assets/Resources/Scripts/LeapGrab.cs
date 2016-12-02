using UnityEngine;
using System.Collections;

// This script defines conditions that are necessary for the Leap player to grab a shared object
// TODO: values of these four boolean variables can be changed either directly here or through other components
// AuthorityManager of a shared object should be notifyed from this script

public class LeapGrab : MonoBehaviour {

    AuthorityManager am_left;
	AuthorityManager am_right;

    // conditions for the object control here
    bool leftHandTouching = false;
    bool rightHandTouching = false;
    bool leftPinch = false;
    bool rightPinch = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if (leftHandTouching && rightHandTouching && leftPinch && rightPinch && am_left.Equals (am_right)) {
			Debug.Log ("TOUCHING");
			// notify AuthorityManager that grab conditions are fulfilled
			am_left.GrabObject ();
		} else if (am_left != null) {
			// grab conditions are not fulfilled
			am_left.UnGrabObject ();
		} else if (am_right != null) {
			am_right.UnGrabObject ();
		}
    }

	public void SetLeftPinchTrue() {
		leftPinch = true;
	}

	public void SetRightPinchTrue() {
		rightPinch = true;
	}

	public void SetLeftPinchFalse() {
		leftPinch = false;
	}

	public void SetRightPinchFalse() {
		rightPinch = false;
	}

	public void SetLeftHandTouching(bool leftHandTouching) {
		this.leftHandTouching = leftHandTouching;
	}

	public void SetRightHandTouching(bool rightHandTouching) {
		this.rightHandTouching = rightHandTouching;
	}

	public void SetAuthorityManagerLeft(AuthorityManager am) {
		this.am_left = am;
	}

	public void SetAuthorityManagerLeftNull() {
		if(am_left != null) am_left.UnGrabObject ();
		this.am_left = null;
	}

	public void SetAuthorityManagerRight(AuthorityManager am) {
		this.am_right = am;
	}

	public void SetAuthorityManagerRightNull() {
		if(am_right != null) am_right.UnGrabObject ();
		this.am_right = null;
	}
}

using UnityEngine;
using System.Collections;

// This script defines conditions that are necessary for the Vive player to grab a shared object
// TODO: values of these four boolean variables can be changed either directly here or through other components
// AuthorityManager of a shared object should be notifyed from this script

public class ViveGrab : MonoBehaviour {

    AuthorityManager am_left; // to communicate the fulfillment of grabbing conditions
	AuthorityManager am_right;
   

    // conditions for the object control here
    bool leftHandTouching = false;
    bool rightHandTouching = false;
    bool leftTriggerDown = false;
    bool rightTriggerDown = false;
    

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
	 
		if (leftHandTouching && rightHandTouching && leftTriggerDown && rightTriggerDown && am_left.Equals (am_right)) {
			Debug.Log ("TOUCHING");
			am_left.GrabObject (null);
			// notify AuthorityManager that grab conditions are fulfilled
		} else if (am_left != null) {
			// grab conditions are not fulfilled
			am_left.UnGrabObject ();
		} else if (am_right != null) {
			am_right.UnGrabObject ();
		}

    }

	public void SetLeftTriggerDown(bool leftTriggerDown) {
		this.leftTriggerDown = leftTriggerDown;
	}

	public void SetRightTriggerDown(bool rightTriggerDown) {
		this.rightTriggerDown = rightTriggerDown;
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
		if(am_left!=null) am_left.UnGrabObject ();
		this.am_left = null;
	}

	public void SetAuthorityManagerRight(AuthorityManager am) {
		this.am_right = am;
	}

	public void SetAuthorityManagerRightNull() {
        if (am_right != null) am_right.UnGrabObject ();
		this.am_right = null;
	}
}

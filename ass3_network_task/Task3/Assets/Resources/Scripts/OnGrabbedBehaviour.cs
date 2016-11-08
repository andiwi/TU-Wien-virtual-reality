﻿using UnityEngine;
using System.Collections;
using Leap.Unity;
using UnityEngine.Networking;
// TODO: define the behaviour of a shared object when it is manipulated by a client

public class OnGrabbedBehaviour : MonoBehaviour
{


    bool grabbed;
    private bool vive;
    private bool leap;

    private GameObject playerController;

    private PinchDetector pinchDetectorL;
    private PinchDetector pinchDetectorR;

    private GameObject controllerL;
    private GameObject controllerR;

    NetworkTransform netTrans;

    // Use this for initialization
    void Start()
    {
        GameObject capsuleHand_L = GameObject.Find("CapsuleHand_L");

        if (capsuleHand_L != null)
        {
            leap = true;
            vive = false;

            GameObject capsuleHand_R = GameObject.Find("CapsuleHand_R");

            pinchDetectorL = capsuleHand_L.GetComponent<PinchDetector>();

            if (capsuleHand_R != null) pinchDetectorR = capsuleHand_R.GetComponent<PinchDetector>();

        }
        else
        {
            controllerL = GameObject.Find("Controller (left)");
            if (controllerL != null)
            {
                leap = false;
                vive = true;

                controllerR = GameObject.Find("Controller (right)");
            }
        }

        netTrans = gameObject.GetComponent<NetworkTransform>();

    }

    private void setupLeapDetectors()
    {
        GameObject capsuleHand_L = GameObject.Find("CapsuleHand_L");
        GameObject capsuleHand_R = GameObject.Find("CapsuleHand_R");

        if (capsuleHand_L != null) pinchDetectorL = capsuleHand_L.GetComponent<PinchDetector>();
        if (capsuleHand_R != null) pinchDetectorR = capsuleHand_R.GetComponent<PinchDetector>();
    }

    private void setupViveDetectors()
    {
        controllerL = GameObject.Find("Controller (left)");
        controllerR = GameObject.Find("Controller (right)");
    }

    // Update is called once per frame
    void Update()
    {

        // GO´s behaviour when it is in a grabbed state (owned by a client) should be defined here
        if (grabbed)
        {
            Debug.Log ("grabbed");
			if (leap) {

				if (pinchDetectorR == null || pinchDetectorL == null) {
					setupLeapDetectors ();
				}

				netTrans.transform.position = (pinchDetectorL.Position + pinchDetectorR.Position) / 2f + new Vector3 (0, 0, 0.2f);
				Debug.Log ("LEAP");
			} else if (vive) {

				if (pinchDetectorR == null || pinchDetectorL == null) {
					setupViveDetectors ();
				}

				netTrans.transform.position = (controllerL.transform.position + controllerR.transform.position) / 2f;
				Debug.Log ("VIVE");
			} else {
				Debug.Log ("SONST WO?");
			}
        }
    }

    public bool IsGrabbed()
    {
        return grabbed;
    }

    // called first time when the GO gets grabbed by a player
    public void OnGrabbed()
    {
        grabbed = true;
    }

    // called when the GO gets released by a player
    public void OnReleased()
    {
        grabbed = false;
    }
}

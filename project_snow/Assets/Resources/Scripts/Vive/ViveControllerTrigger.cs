using UnityEngine;
using System.Collections;
using Valve.VR;

public class ViveControllerTrigger : MonoBehaviour {

	public bool leftController;
	public bool rightController;

	SteamVR_TrackedController controller;
	//private ViveGrab viveGrab;
    private ViveSnowBallControl snowBallCtrl;

	// Use this for initialization
	void Start () {
		controller = GetComponent<SteamVR_TrackedController>();

        //GameObject playerController = GameObject.Find ("PlayerController");
        //viveGrab = playerController.GetComponent<ViveGrab> ();
        snowBallCtrl = GetComponent<ViveSnowBallControl>();

		controller.TriggerClicked += new ClickedEventHandler(SetTriggerClicked);
		controller.TriggerUnclicked += new ClickedEventHandler(SetTriggerUnclicked);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetTriggerClicked(object sender, ClickedEventArgs e) {
        //if (leftController) {
        //	viveGrab.SetLeftTriggerDown (true);
        //} else if (rightController) {
        //	viveGrab.SetRightTriggerDown (true);
        //}

        print("SetTriggerClicked ");

	}

	private void SetTriggerUnclicked(object sender, ClickedEventArgs e) {
        //if (leftController) {
        //	viveGrab.SetLeftTriggerDown (false);
        //} else if (rightController) {
        //	viveGrab.SetRightTriggerDown (false);
        //}

        print("SetTriggerUnclicked ");
    }
}

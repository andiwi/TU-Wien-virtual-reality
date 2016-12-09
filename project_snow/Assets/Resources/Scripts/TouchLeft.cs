using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// TODO: this script CAN be used to detect the events of a left networked hand touching a shared object
// fill in the implementation and communicate touching events to either LeapGrab and ViveGrab by setting the leftHandTouching variable
// ALTERNATIVELY, implement the verification of the grabbing conditions in a way  your prefer
// TO REMEMBER: only the localPlayer (networked hands belonging to the localPlayer) should be able to "touch" shared objects

public class TouchLeft : MonoBehaviour
{

    public bool vive;
    public bool leap;
    Actor thisActor;

    void Start()
    {


    }
    void OnTriggerEnter(Collider other)
    {
        if (thisActor == null)
        {
            thisActor = gameObject.transform.parent.parent.gameObject.GetComponent<Actor>();
        }
        if (thisActor.isLocalPlayer == false) return;
        GameObject playerController = GameObject.FindGameObjectWithTag("PlayerController");
        if (playerController == null)
        {
            print("TouchLeft - no playercontroller - abort");
            return;
        }

        AuthorityManager am = other.gameObject.GetComponent<AuthorityManager>();

        if (vive)
        {
            handleViveOnTriggerEnter(playerController, am);

        }
        else if (leap)
        {
            LeapGrab leapGrab = playerController.GetComponent<LeapGrab>();
            leapGrab.SetLeftHandTouching(true);
            leapGrab.SetAuthorityManagerLeft(am);
        }
    }



    void OnTriggerExit()
    {
        if (thisActor == null)
        {
            thisActor = gameObject.transform.parent.parent.gameObject.GetComponent<Actor>();
        }
        if (thisActor.isLocalPlayer == false) return;
        GameObject playerController = GameObject.FindGameObjectWithTag("PlayerController");
        if (playerController == null)
        {
            print("TouchLeft - no playercontroller - abort");
            return;
        }

        if (vive)
        {
            handleViveOnTriggerExit(playerController);
        }
        else if (leap)
        {
            LeapGrab leapGrab = playerController.GetComponent<LeapGrab>();
            leapGrab.SetLeftHandTouching(false);
            leapGrab.SetAuthorityManagerLeftNull();
        }
    }


    private void handleViveOnTriggerEnter(GameObject playerController, AuthorityManager am)
    {
        if (am == null) return;

        GameObject leftCtrl = playerController.transform.Find("Controller (left)").gameObject;
        if (leftCtrl == null) return;
        print("found leftCtrl!");

        ViveSnowBallControl snowBallCtrl = leftCtrl.GetComponent<ViveSnowBallControl>();
        snowBallCtrl.SetTouchingSnowBall(true, am);
    }

    private void handleViveOnTriggerExit(GameObject playerController)
    {

        GameObject leftCtrl = playerController.transform.Find("Controller (left)").gameObject;
        if (leftCtrl == null) return;
        print("found leftCtrl!");

        ViveSnowBallControl snowBallCtrl = leftCtrl.GetComponent<ViveSnowBallControl>();
        snowBallCtrl.SetTouchingSnowBall(false, null);
    }


}

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
        if(thisActor == null)
        {
            thisActor = gameObject.transform.parent.parent.gameObject.GetComponent<Actor>();
        }
        if (thisActor.isLocalPlayer == false) return;
        GameObject playerController = GameObject.Find("PlayerController");
        if (playerController == null)
        {
            return;
        }

        AuthorityManager am = other.gameObject.GetComponent<AuthorityManager>();

        if (vive)
        {
            ViveGrab viveGrab = playerController.GetComponent<ViveGrab>();
            viveGrab.SetLeftHandTouching(true);
            viveGrab.SetAuthorityManagerLeft(am);

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
        GameObject playerController = GameObject.Find("PlayerController");
        if (playerController == null)
        {
            return;
        }

        if (vive)
        {
            ViveGrab viveGrab = playerController.GetComponent<ViveGrab>();
            viveGrab.SetLeftHandTouching(false);
            viveGrab.SetAuthorityManagerLeftNull();

        }
        else if (leap)
        {
            LeapGrab leapGrab = playerController.GetComponent<LeapGrab>();
            leapGrab.SetLeftHandTouching(false);
            leapGrab.SetAuthorityManagerLeftNull();
        }
    }

}

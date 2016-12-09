using UnityEngine;
using System.Collections;
using Leap.Unity;
using UnityEngine.Networking;
// TODO: define the behaviour of a shared object when it is manipulated by a client

public class OnGrabbedBehaviour : MonoBehaviour
{

    bool grabbed;

    private GameObject playerController;

    private PinchDetector pinchDetectorL;
    private PinchDetector pinchDetectorR;

    private GameObject controllerL;
    private GameObject controllerR;

    void Start()
    {

    }

    void Update()
    {
        // GO´s behaviour when it is in a grabbed state (owned by a client) should be defined here
        if (grabbed)
        {
            Debug.Log("grabbed");
        }
    }

    public bool IsGrabbed()
    {
        return grabbed;
    }

    // called first time when the GO gets grabbed by a player
    public void OnGrabbed(Transform parent)
    {
        grabbed = true;
        transform.SetParent(parent);
    }

    // called when the GO gets released by a player
    public void OnReleased()
    {
        grabbed = false;
        transform.SetParent(null);
    }
}

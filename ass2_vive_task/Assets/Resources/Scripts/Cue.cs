using UnityEngine;
using System.Collections;

public class Cue : MonoBehaviour
{

    Vector3 oldpos;
    Vector3 newpos;
    Vector3 velocity;

    Rigidbody rigidBody;
    // Use this for initialization

    //public SteamVR_Controller.Device frontDevice;
    //public SteamVR_Controller.Device backDevice;


    /**
     * sorted list of the controllers - sorted by the distance to the tip 
     * used to easily determine front and back device
     * */
    System.Collections.Generic.SortedList<float, SteamVR_Controller.Device> controllers;

    bool twoControllerMode = false;


    public void AttachDevice(SteamVR_Controller.Device device)
    {
        Vector3 tipPos = new Vector3(0, transform.localScale.y) + transform.position;

        if (!controllers.ContainsValue(device))
        {
            float distanceFromTip = (tipPos - device.transform.pos).magnitude;
            Debug.Log("attaching device - distanceFromTip: " + distanceFromTip);
            controllers.Add(distanceFromTip, device);
        }
        else
        {
            Debug.Log("Tried to attachDevice - but device is already attached");
        }
    }

    public void DetachDevice(SteamVR_Controller.Device device)
    {
        if (controllers.ContainsValue(device))
        {
            controllers.Values.Remove(device);

            Debug.Log("removed device controllers size is now: " + controllers.Count);
        }
        else
        {
            Debug.Log("Tried to detachDevice - but device not attached");
        }
    }

    void Start()
    {
        controllers = new System.Collections.Generic.SortedList<float, SteamVR_Controller.Device>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        oldpos = transform.position;
    }


    void Update()
    {
        changePosition();
    }

    void FixedUpdate()
    {
        calcVelocity();
    }

    private SteamVR_Controller.Device getBackDevice()
    {
        if (hasOneControllerAttached())
        {
            return controllers.Values[0];
        }
        else if (hasTwoControllersAttached())
        {
            return controllers.Values[1];
        }
        else
        {
            Debug.Log("getBackDevice no device!");
            return null;
        }
    }

    private SteamVR_Controller.Device getFrontDevice()
    {
        if (hasTwoControllersAttached())
        {
            return controllers.Values[0];
        }
        else
        {
            Debug.Log("getFrontDevice no device!");
            return null;
        }
    }

    private bool hasTwoControllersAttached()
    {
        return controllers.Count == 2;
    }

    private bool hasOneControllerAttached()
    {
        return controllers.Count == 1;
    }

    private void changePosition()
    {
        //transform.Rotate(frontDevice.transform - backDevice.transform);

        if (hasTwoControllersAttached())
        {
            Vector3 dir = getFrontDevice().transform.pos - getBackDevice().transform.pos;
            Vector3 mid = dir / 2.0f + getBackDevice().transform.pos;

            //transform.position = mid;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        } 


    }

    /**
     * calculates velocity since it is kinematic and has no velocity to apply on collision
     * */
    private void calcVelocity()
    {
        newpos = transform.position;
        velocity = (newpos - oldpos) / Time.fixedDeltaTime;
        oldpos = newpos;
        newpos = transform.position;
    }


    public float forceStrength;

    void OnCollisionEnter(Collision collision)
    {

        //Vector3 dir = collision.contacts[0].point - transform.position;


        Rigidbody rigidCollider = collision.rigidbody;


        Vector3 force = velocity.normalized * forceStrength;
        //Vector3 force = dir.normalized * forceStrength;

        // rigidCollider.AddForce(dir.normalized * forceStrength, ForceMode.Impulse);

        rigidCollider.AddForce(force, ForceMode.Force);

        Debug.Log("HIT something with cue! adding force: " + force);
        //rigidCollider.velocity = rigidBody.velocity;
        //rigidCollider.angularVelocity = rigidBody.angularVelocity;
    }

    /*
     *             rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;

    */

    /*
     * 
     *      public var explosionStrength : float = 10.0f;

     function OnTriggerEnter (target_ : Collider)
     {
         var forceVec : Vector3 = -target_.rigidbody.velocity.normalized * explosionStrength;
         target_.rigidbody.AddForce(forceVec,ForceMode.Acceleration);
     }*/
}

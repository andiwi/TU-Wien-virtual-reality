using UnityEngine;
using System.Collections;

public class CueControl : MonoBehaviour, IViveControlControllable
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
    System.Collections.Generic.SortedList<float, ViveControllerControl> controllers;

    bool twoControllerMode = false;



    void Start()
    {
        controllers = new System.Collections.Generic.SortedList<float, ViveControllerControl>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        oldpos = transform.position;
    }


    void Update()
    {
        //changePosition();
    }

    void FixedUpdate()
    {
        calcVelocity();
        
    }

    FixedJoint fix;

    public void AttachController(ViveControllerControl viveController)
    {
        if (!isControllerAttached(viveController))
        {
            Vector3 tipPos = new Vector3(0, transform.localScale.y) + transform.position; //TODO optimize

            float distanceFromTip = (tipPos - deviceGameObject.transform.position).magnitude;
            //Controller newController = new Controller(device, parentableTransform);
            controllers.Add(distanceFromTip, deviceGameObject);
            Debug.Log("attaching device - distanceFromTip: " + distanceFromTip + " , controllers size is now: " + controllers.Count);

            if (controllers.Count == 1)
            {
                fix = gameObject.AddComponent<FixedJoint>();
                fix.connectedBody = deviceGameObject.GetComponent<Rigidbody>();
                //fix.autoConfigureConnectedAnchor = false;
                // fix.connectedAnchor = deviceGameObject.transform.position;
                fix.anchor = Vector3.zero;
                //fix.connectedAnchor = Vector3.zero;
            }
        }
        else
        {
            Debug.Log("Tried to attachDevice - but device is already attached");
        }
    }

    public void DetachController(ViveControllerControl viveController)
    {

        //Controller foundController = getControllerByDevice(device);

        Debug.Log("TODO reimplement");

        //if (foundController != null)
        //{
        //    controllers.RemoveAt(controllers.IndexOfValue(foundController));
        //    //controllers.Values.Remove(device); //TODO work on removing..

        //    Debug.Log("removed device controllers size is now: " + controllers.Count);
        //}
        //else
        //{
        //    Debug.Log("Tried to detachDevice - but device not attached");
        //}
    }



    private bool isControllerAttached(ViveControllerControl viveController)
    {
        return controllers.ContainsValue(viveController);
        //Controller foundController = getControllerByDevice(device);
        //return foundController != null;
    }

    //private Controller getControllerByDevice(SteamVR_Controller.Device device)
    //{
    //    foreach (System.Collections.Generic.KeyValuePair<float, Controller> curr in controllers)
    //    {
    //        if (curr.Value.device.Equals(device))
    //        {
    //            return curr.Value;
    //        }
    //    }

    //    return null;
    //}




    private GameObject getBackController()
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
            Debug.Log("getBackController no device!");
            return null;
        }
    }

    private GameObject getFrontController()
    {
        if (hasTwoControllersAttached())
        {
            return controllers.Values[0];
        }
        else
        {
            Debug.Log("getFrontController no device!");
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
            //transform.parent = null;
            //Vector3 dir = (getFrontController().device.transform.pos - getBackController().device.transform.pos).normalized; 
            //Vector3 dir = (getFrontController().device.transform.pos - getBackController().device.transform.pos);
            Vector3 dir = getFrontController().transform.position - getBackController().transform.position;

            //Debug.Log("changePos: frontPos: " + getFrontController().device.transform.pos + ", backPos: " + getBackController().device.transform.pos + ", dir: " + dir);
            Debug.Log("changePos: frontPos: " + getFrontController().transform.position + ", backPos: " + getBackController().transform.position + ", dir: " + dir);

            //Quaternion rotation = Quaternion.LookRotation(dir);



            //float rotationSpeed = 1f; //test

            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            //transform.rotation = Quaternion.LookRotation(dir.normalized);

            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            //transform.localRotation = Quaternion.LookRotation(dir.normalized);
            //transform.LookAt(getFrontController().controllerTransform.position);

            //transform.Rotate(Quaternion.LookRotation(dir.normalized));
            //Quaternion.

            //fix.connectedAnchor = getBackController().transform.position;

            Debug.Log("connectedAnchorPos: " +  fix.connectedAnchor);

            //test1: 
            //Vector3 testBack = getBackController().device.transform.pos - transform.position;
            //Vector3 testFront = getFrontController().device.transform.pos - transform.position;
            //transform.position = transform.position + testBack + testFront;
            //Debug.Log("testBackVec: " + testBack + ", testFrontVec: " + testFront + ", new posVecotr: " + transform.position);

            //test2:
            //Vector3 mid = dir / 2.0f + getBackController().device.transform.pos;        
            //transform.position = mid + transform.position;


            //Vector3 newPos = (transform.position - getBackController().controllerTransform.position);
            //transform.localPosition = tran

            //Vector3 mid = dir / 2.0f + getBackController().controllerTransform.position;
            //Debug.Log("new mid: " + mid);
            //transform.position = mid;

            //transform.localPosition;


            //transform.position = mid;
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

            //transform.SetParent(getBackController().controllerTransform);

            //foo.transform.position = mainCamera.transform.position + mainCamera.transform.forward * Vector3.Distance(mainCamera.transform.position, foo.transform.position);



        }
        else if (hasOneControllerAttached())
        {
            //transform.SetParent(getBackController().controllerTransform);
        }
        else
        {
            //transform.parent = null;
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
}

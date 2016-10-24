using UnityEngine;
using System.Collections;

public class CueControl : MonoBehaviour, IViveControlControllable
{

    Vector3 oldpos;
    Vector3 newpos;
    Vector3 velocity;

    public float forceStrength;

    ViveControllerControl attachedController;
    

    void Start()
    {
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

    public void AttachController(ViveControllerControl viveController)
    {
        attachedController = viveController;
        attachedController.getOrientationHelber().createFixedJoint(GetComponent<Rigidbody>());
    }

    public bool isControllerAttached()
    {
        return attachedController != null;
    }


    public void DetachController()
    {
        if (attachedController != null)
        {
            attachedController.getOrientationHelber().destroyFixedJoint(GetComponent<Rigidbody>());
            attachedController = null;
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

    void OnCollisionEnter(Collision collision)
    {

        //Vector3 dir = collision.contacts[0].point - transform.position;

        Debug.Log("CueControl HIT " + collision.gameObject.name);

        if (collision.rigidbody)
        {
            Rigidbody rigidCollider = collision.rigidbody;

            if (isControllerAttached())
            {
                attachedController.RumbleController(0.2f, 0.6f);
            }

            Vector3 force = velocity * forceStrength;
            //Vector3 force = velocity.normalized * forceStrength;

            rigidCollider.AddForce(force, ForceMode.Force);

            Debug.Log(" with cue! adding force: " + force);

        }
    }
}

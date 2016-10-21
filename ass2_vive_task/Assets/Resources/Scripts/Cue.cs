using UnityEngine;
using System.Collections;

public class Cue : MonoBehaviour {

    Vector3 oldpos;
    Vector3 newpos;
    Vector3 velocity;

    Rigidbody rigidBody;
    // Use this for initialization


    public SteamVR_Controller.Device frontDevice;
    public SteamVR_Controller.Device backDevice;



    void Start () {

        rigidBody = gameObject.GetComponent<Rigidbody>();
        oldpos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        changePosition();
    }

    void FixedUpdate ()
    {
        calcVelocity();
    }

    private void changePosition()
    {
        //transform.Rotate(frontDevice.transform - backDevice.transform);
        Vector3 dir = frontDevice.transform.pos - backDevice.transform.pos;
        Vector3 mid = dir / 2.0f + backDevice.transform.pos;

        //transform.position = mid;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

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

    public void AttachFrontDevice(SteamVR_Controller.Device frontDevice)
    {
        this.frontDevice = frontDevice;
    }


    public void AttackBackDevice(SteamVR_Controller.Device backDevice)
    {
        this.backDevice = backDevice;
    }

    public float forceStrength;

    void OnCollisionEnter(Collision collision)
    {

        Vector3 dir = collision.contacts[0].point - transform.position;
        

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

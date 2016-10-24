using UnityEngine;
using System.Collections;

public class OrientationHelper : MonoBehaviour {
    public GameObject otherController;

	void Start () {	
  
	}

    public void createFixedJoint(Rigidbody connectedBody)
    {
        
        Rigidbody rigid = gameObject.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
        rigid.useGravity = false;

        //set initial rotation
        connectedBody.transform.rotation = transform.rotation;
        Debug.Log("orientation: " + transform.rotation);

        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = connectedBody;
        //joint.anchor = transform.position;
        Debug.Log("setFixedJoint for " + connectedBody.name);
    }

    public void destroyFixedJoint(Rigidbody connectedBody)
    {
        Destroy(gameObject.GetComponent<FixedJoint>());
        Destroy(gameObject.GetComponent<Rigidbody>());
    }


    void Update () {
        Vector3 dir = otherController.transform.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

}

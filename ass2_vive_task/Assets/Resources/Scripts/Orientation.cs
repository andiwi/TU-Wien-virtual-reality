using UnityEngine;
using System.Collections;

public class Orientation : MonoBehaviour {

    public GameObject otherController;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = otherController.transform.position - transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    public Quaternion getRotation()
    {
        return transform.rotation;
    }
}

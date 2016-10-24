using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    public GameObject point1;
    public GameObject point2;
    public GameObject thisObj;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 dir = point1.transform.position - point2.transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);


    }
}

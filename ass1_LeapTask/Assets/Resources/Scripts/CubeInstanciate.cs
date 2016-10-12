using UnityEngine;
using System.Collections;

public class CubeInstanciate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.AddComponent<Rigidbody>();
		cube.transform.position = new Vector3(4, 1, 1);
		Debug.Log ("hello");
		*/
	}

	public void CreateObject() {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.AddComponent<Rigidbody>();
		cube.transform.position = new Vector3(4, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

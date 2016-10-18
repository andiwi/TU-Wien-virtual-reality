using UnityEngine;
using System.Collections;

public class Balls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject ball1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball1.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball1.transform.position = new Vector3 (0, 0, 0);

		GameObject ball2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball2.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball2.transform.position = new Vector3 (-0.5f, 0, 0);

		GameObject ball3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball3.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball3.transform.position = new Vector3 (0.5f, 0, 0);

		GameObject ball4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball4.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball4.transform.position = new Vector3 (0, 0, 0.5f);

		GameObject ball5 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball5.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball5.transform.position = new Vector3 (-0.5f, 0, 0.5f);

		GameObject ball6 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball6.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball6.transform.position = new Vector3 (0.5f, 0, 0.5f);

		GameObject ball7 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball7.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball7.transform.position = new Vector3 (0, 0, -0.5f);

		GameObject ball8 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball8.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball8.transform.position = new Vector3 (-0.5f, 0, -0.5f);

		GameObject ball9 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball9.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball9.transform.position = new Vector3 (0.5f, 0, -0.5f);

		GameObject ball10 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball10.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball10.transform.position = new Vector3 (-0.25f, 0.38f, -0.25f);

		GameObject ball11 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball11.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball11.transform.position = new Vector3 (0.25f, 0.38f, -0.25f);

		GameObject ball12 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball12.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball12.transform.position = new Vector3 (-0.25f, 0.38f, 0.25f);

		GameObject ball13 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball13.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball13.transform.position = new Vector3 (0.25f, 0.38f, 0.25f);

		GameObject ball14 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball14.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball14.transform.position = new Vector3 (0, 0.76f, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

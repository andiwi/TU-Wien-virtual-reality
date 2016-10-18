using UnityEngine;
using System.Collections;

public class GamingBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box.transform.localScale += new Vector3 (2, 2, 2);

		GameObject hole1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole1.transform.position = new Vector3 (-1.5f, -1.5f, -1.5f);

		GameObject hole2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole2.transform.position = new Vector3 (-1.5f, -1.5f, 1.5f);

		GameObject hole3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole3.transform.position = new Vector3 (-1.5f, 1.5f, -1.5f);

		GameObject hole4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole4.transform.position = new Vector3 (-1.5f, 1.5f, 1.5f);

		GameObject hole5 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole5.transform.position = new Vector3 (1.5f, -1.5f, -1.5f);

		GameObject hole6 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole6.transform.position = new Vector3 (1.5f, -1.5f, 1.5f);

		GameObject hole7 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole7.transform.position = new Vector3 (1.5f, 1.5f, -1.5f);

		GameObject hole8 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole8.transform.position = new Vector3 (1.5f, 1.5f, 1.5f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

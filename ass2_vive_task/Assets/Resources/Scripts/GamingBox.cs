using UnityEngine;
using System.Collections;

public class GamingBox : MonoBehaviour {
	public Material boxMaterial;
	public Material holeMaterial;

	// Use this for initialization
	void Start () {
		GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box.transform.localScale += new Vector3 (2, 2, 2);
		box.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		GameObject hole1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole1.transform.position = new Vector3 (-1.5f, -1.5f, -1.5f);
		hole1.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole2.transform.position = new Vector3 (-1.5f, -1.5f, 1.5f);
		hole2.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole3.transform.position = new Vector3 (-1.5f, 1.5f, -1.5f);
		hole3.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole4.transform.position = new Vector3 (-1.5f, 1.5f, 1.5f);
		hole4.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole5 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole5.transform.position = new Vector3 (1.5f, -1.5f, -1.5f);
		hole5.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole6 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole6.transform.position = new Vector3 (1.5f, -1.5f, 1.5f);
		hole6.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole7 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole7.transform.position = new Vector3 (1.5f, 1.5f, -1.5f);
		hole7.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		GameObject hole8 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole8.transform.position = new Vector3 (1.5f, 1.5f, 1.5f);
		hole8.GetComponent<Renderer>().sharedMaterial = holeMaterial;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

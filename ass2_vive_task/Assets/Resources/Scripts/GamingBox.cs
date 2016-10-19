using UnityEngine;
using System.Collections;

public class GamingBox : MonoBehaviour {
	public Material boxMaterial;
	public Material holeMaterial;

	// Use this for initialization
	void Start () {
		GameObject box1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box1.transform.localScale = new Vector3 (3, 3, 0.1f);
		box1.transform.position = new Vector3(0, 0,-1.5f);
		box1.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box2.transform.localScale = new Vector3 (3, 3, 0.1f);
		box2.transform.position = new Vector3(0, 0, 1.5f);
		box2.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box3.transform.localScale = new Vector3 (3, 0.1f, 3);
		box3.transform.position = new Vector3(0, 1.5f, 0);
		box3.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box4.transform.localScale = new Vector3 (3, 0.1f, 3);
		box4.transform.position = new Vector3(0, -1.5f, 0);
		box4.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		GameObject box5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box5.transform.localScale = new Vector3 (0.1f, 3, 3);
		box5.transform.position = new Vector3(-1.5f, 0, 0);
		box5.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		GameObject box6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box6.transform.localScale = new Vector3 (0.1f, 3, 3);
		box6.transform.position = new Vector3(1.5f, 0, 0);
		box6.GetComponent<Renderer>().sharedMaterial = boxMaterial;

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

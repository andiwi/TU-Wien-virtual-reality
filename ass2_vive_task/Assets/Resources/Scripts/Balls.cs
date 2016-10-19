using UnityEngine;
using System.Collections;

public class Balls : MonoBehaviour {

	public Material ball0_material;
	public Material ball1_material;
	public Material ball2_material;
	public Material ball3_material;
	public Material ball4_material;
	public Material ball5_material;
	public Material ball6_material;
	public Material ball7_material;
	public Material ball8_material;
	public Material ball9_material;
	public Material ball10_material;
	public Material ball11_material;
	public Material ball12_material;
	public Material ball13_material;
	public Material ball14_material;

	// Use this for initialization
	void Start () {
		GameObject ball0 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball0.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball0.transform.position = new Vector3 (0, 0.5f, 1.5f);
		ball0.GetComponent<Renderer> ().sharedMaterial = ball0_material;

		GameObject ball1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball1.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball1.transform.position = new Vector3 (-0.5f, 0, -0.5f);
		//Material ball1_material= Resources.Load("Resources/Materials/Ball_01.mat", typeof(Material)) as Material;
		ball1.GetComponent<Renderer> ().sharedMaterial = ball1_material;

		GameObject ball2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball2.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball2.transform.position = new Vector3 (-0.5f, 0, 0);
		ball2.GetComponent<Renderer> ().sharedMaterial = ball2_material;

		GameObject ball3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball3.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball3.transform.position = new Vector3 (0.5f, 0, 0);
		ball3.GetComponent<Renderer> ().sharedMaterial = ball3_material;

		GameObject ball4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball4.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball4.transform.position = new Vector3 (0, 0, 0.5f);
		ball4.GetComponent<Renderer> ().sharedMaterial = ball4_material;

		GameObject ball5 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball5.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball5.transform.position = new Vector3 (-0.5f, 0, 0.5f);
		ball5.GetComponent<Renderer> ().sharedMaterial = ball5_material;

		GameObject ball6 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball6.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball6.transform.position = new Vector3 (0.5f, 0, 0.5f);
		ball6.GetComponent<Renderer> ().sharedMaterial = ball6_material;

		GameObject ball7 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball7.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball7.transform.position = new Vector3 (0, 0, -0.5f);
		ball7.GetComponent<Renderer> ().sharedMaterial = ball7_material;

		GameObject ball8 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball8.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball8.transform.position = new Vector3 (0, 0, 0);
		ball8.GetComponent<Renderer> ().sharedMaterial = ball8_material;

		GameObject ball9 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball9.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball9.transform.position = new Vector3 (0.5f, 0, -0.5f);
		ball9.GetComponent<Renderer> ().sharedMaterial = ball9_material;

		GameObject ball10 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball10.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball10.transform.position = new Vector3 (-0.25f, 0.38f, -0.25f);
		ball10.GetComponent<Renderer> ().sharedMaterial = ball10_material;

		GameObject ball11 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball11.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball11.transform.position = new Vector3 (0.25f, 0.38f, -0.25f);
		ball11.GetComponent<Renderer> ().sharedMaterial = ball11_material;

		GameObject ball12 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball12.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball12.transform.position = new Vector3 (-0.25f, 0.38f, 0.25f);
		ball12.GetComponent<Renderer> ().sharedMaterial = ball12_material;

		GameObject ball13 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball13.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball13.transform.position = new Vector3 (0.25f, 0.38f, 0.25f);
		ball13.GetComponent<Renderer> ().sharedMaterial = ball13_material;

		GameObject ball14 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball14.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball14.transform.position = new Vector3 (0, 0.76f, 0);
		ball14.GetComponent<Renderer> ().sharedMaterial = ball14_material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

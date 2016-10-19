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

	private GameObject cue_ball;

	// Use this for initialization
	void Start () {
		cue_ball = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		cue_ball.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		cue_ball.transform.position = new Vector3 (0, 0.5f, -1.5f);
		cue_ball.GetComponent<Renderer> ().sharedMaterial = ball0_material;

		Rigidbody rb = cue_ball.AddComponent<Rigidbody>();
		rb.mass = 2;
		rb.useGravity = false;

		GameObject ball1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball1.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball1.transform.position = new Vector3 (-0.5f, 0, -0.5f);
		//Material ball1_material= Resources.Load("Resources/Materials/Ball_01.mat", typeof(Material)) as Material;
		ball1.GetComponent<Renderer> ().sharedMaterial = ball1_material;
		Rigidbody rb1 = ball1.AddComponent<Rigidbody>();
		rb1.useGravity = false;

		GameObject ball2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball2.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball2.transform.position = new Vector3 (-0.5f, 0, 0);
		ball2.GetComponent<Renderer> ().sharedMaterial = ball2_material;
		Rigidbody rb2 = ball2.AddComponent<Rigidbody>();
		rb2.useGravity = false;

		GameObject ball3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball3.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball3.transform.position = new Vector3 (0.5f, 0, 0);
		ball3.GetComponent<Renderer> ().sharedMaterial = ball3_material;
		Rigidbody rb3 = ball3.AddComponent<Rigidbody>();
		rb3.useGravity = false;

		GameObject ball4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball4.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball4.transform.position = new Vector3 (0, 0, 0.5f);
		ball4.GetComponent<Renderer> ().sharedMaterial = ball4_material;
		Rigidbody rb4 = ball4.AddComponent<Rigidbody>();
		rb4.useGravity = false;

		GameObject ball5 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball5.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball5.transform.position = new Vector3 (-0.5f, 0, 0.5f);
		ball5.GetComponent<Renderer> ().sharedMaterial = ball5_material;
		Rigidbody rb5 = ball5.AddComponent<Rigidbody>();
		rb5.useGravity = false;

		GameObject ball6 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball6.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball6.transform.position = new Vector3 (0.5f, 0, 0.5f);
		ball6.GetComponent<Renderer> ().sharedMaterial = ball6_material;
		Rigidbody rb6 = ball6.AddComponent<Rigidbody>();
		rb6.useGravity = false;

		GameObject ball7 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball7.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball7.transform.position = new Vector3 (0, 0, -0.5f);
		ball7.GetComponent<Renderer> ().sharedMaterial = ball7_material;
		Rigidbody rb7 = ball7.AddComponent<Rigidbody>();
		rb7.useGravity = false;

		GameObject ball8 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball8.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball8.transform.position = new Vector3 (0, 0, 0);
		ball8.GetComponent<Renderer> ().sharedMaterial = ball8_material;
		Rigidbody rb8 = ball8.AddComponent<Rigidbody>();
		rb8.useGravity = false;

		GameObject ball9 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball9.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball9.transform.position = new Vector3 (0.5f, 0, -0.5f);
		ball9.GetComponent<Renderer> ().sharedMaterial = ball9_material;
		Rigidbody rb9 = ball9.AddComponent<Rigidbody>();
		rb9.useGravity = false;

		GameObject ball10 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball10.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball10.transform.position = new Vector3 (-0.25f, 0.38f, -0.25f);
		ball10.GetComponent<Renderer> ().sharedMaterial = ball10_material;
		Rigidbody rb10 = ball10.AddComponent<Rigidbody>();
		rb10.useGravity = false;

		GameObject ball11 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball11.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball11.transform.position = new Vector3 (0.25f, 0.38f, -0.25f);
		ball11.GetComponent<Renderer> ().sharedMaterial = ball11_material;
		Rigidbody rb11 = ball11.AddComponent<Rigidbody>();
		rb11.useGravity = false;

		GameObject ball12 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball12.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball12.transform.position = new Vector3 (-0.25f, 0.38f, 0.25f);
		ball12.GetComponent<Renderer> ().sharedMaterial = ball12_material;
		Rigidbody rb12 = ball12.AddComponent<Rigidbody>();
		rb12.useGravity = false;

		GameObject ball13 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball13.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball13.transform.position = new Vector3 (0.25f, 0.38f, 0.25f);
		ball13.GetComponent<Renderer> ().sharedMaterial = ball13_material;
		Rigidbody rb13 = ball13.AddComponent<Rigidbody>();
		rb13.useGravity = false;

		GameObject ball14 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball14.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball14.transform.position = new Vector3 (0, 0.76f, 0);
		ball14.GetComponent<Renderer> ().sharedMaterial = ball14_material;
		Rigidbody rb14 = ball14.AddComponent<Rigidbody>();
		rb14.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () {
		cue_ball.GetComponent<Rigidbody> ().AddForce (transform.forward * 10);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public PhysicMaterial ball_collider_material;

	private GameObject cue_ball;
	private List<GameObject> balls = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
		cue_ball = this.createBall(new Vector3 (0, 0.5f, -1.5f), ball0_material, "CueBall");

		balls.Add(this.createBall(new Vector3 (-0.5f, 0, -0.5f), ball1_material, "ball1"));
		balls.Add(this.createBall(new Vector3 (-0.5f, 0, 0), ball2_material, "ball2"));
		balls.Add(this.createBall(new Vector3 (0.5f, 0, 0), ball3_material, "ball3"));
		balls.Add(this.createBall(new Vector3 (0, 0, 0.5f), ball4_material, "ball4"));
		balls.Add(this.createBall(new Vector3 (-0.5f, 0, 0.5f), ball5_material, "ball5"));
		balls.Add(this.createBall(new Vector3 (0.5f, 0, 0.5f), ball6_material, "ball6"));
		balls.Add(this.createBall(new Vector3 (0, 0, -0.5f), ball7_material, "ball7"));
		balls.Add(this.createBall(new Vector3 (0, 0, 0), ball8_material, "ball8"));
		balls.Add(this.createBall(new Vector3 (0.5f, 0, -0.5f), ball9_material, "ball9"));
		balls.Add(this.createBall(new Vector3 (-0.25f, 0.38f, -0.25f), ball10_material, "ball10"));
		balls.Add(this.createBall(new Vector3 (0.25f, 0.38f, -0.25f), ball11_material, "ball11"));
		balls.Add(this.createBall(new Vector3 (-0.25f, 0.38f, 0.25f), ball12_material, "ball12"));
		balls.Add(this.createBall(new Vector3 (0.25f, 0.38f, 0.25f), ball13_material, "ball13"));
		balls.Add(this.createBall(new Vector3 (0, 0.76f, 0), ball14_material, "ball14"));

		//cue_ball.GetComponent<Rigidbody> ().AddForce (transform.forward * 1000);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private GameObject createBall(Vector3 position, Material material, string name) {
		GameObject ball = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		ball.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		ball.transform.position = position;
		ball.tag = "Ball";
		ball.name = name;
		ball.transform.parent = GameObject.Find("Balls").transform;
		ball.GetComponent<Renderer> ().sharedMaterial = material;
		ball.GetComponent<Collider>().material = ball_collider_material;

		Rigidbody rb = ball.AddComponent<Rigidbody>();
		rb.drag = 0.7f;
		rb.angularDrag = 0.5f;
		rb.useGravity = false;

		return ball;
	}
}

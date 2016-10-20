using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamingBox : MonoBehaviour {
	public Material boxMaterial;
	public Material holeMaterial;

	private List<GameObject> walls = new List<GameObject>();
	private List<GameObject> holes = new List<GameObject>();

	// Use this for initialization
	void Start () {

		walls.Add (createWall (new Vector3 (7, 7, 0.1f), new Vector3 (0, 0, -3.5f), "wall_front"));
		walls.Add (createWall (new Vector3 (7, 7, 0.1f), new Vector3 (0, 0, 3.5f), "wall_back"));
		walls.Add (createWall (new Vector3 (7, 0.1f, 7), new Vector3 (0, 3.5f, 0), "wall_top"));
		walls.Add (createWall (new Vector3 (7, 0.1f, 7), new Vector3 (0, -3.5f, 0), "wall_bottom"));
		walls.Add (createWall (new Vector3 (0.1f, 7, 7), new Vector3 (-3.5f, 0, 0), "wall_left"));
		walls.Add (createWall (new Vector3 (0.1f, 7, 7), new Vector3 (3.5f, 0, 0), "wall_right"));

		holes.Add(createHole(new Vector3(-3.5f, -3.5f, -3.5f), "wall_left_front_bottom"));
		holes.Add(createHole(new Vector3(-3.5f, -3.5f, 3.5f), "wall_left_back_bottom"));
		holes.Add(createHole(new Vector3(-3.5f, 3.5f, -3.5f), "wall_left_front_top"));
		holes.Add(createHole(new Vector3(-3.5f, 3.5f, 3.5f), "wall_left_back_top"));
		holes.Add(createHole(new Vector3(3.5f, -3.5f, -3.5f), "wall_right_front_bottom"));
		holes.Add(createHole(new Vector3(3.5f, -3.5f, 3.5f), "wall_right_back_bottom"));
		holes.Add(createHole(new Vector3(3.5f, 3.5f, -3.5f), "wall_right_front_top"));
		holes.Add(createHole(new Vector3(3.5f, 3.5f, 3.5f), "wall_right_back_bottom"));

		//holes
		/*
		GameObject hole1 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole1.transform.position = new Vector3 (-3.5f, -3.5f, -3.5f);
		hole1.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole1 = hole1.AddComponent<Rigidbody>();
		rb_hole1.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole2.transform.position = new Vector3 (-3.5f, -3.5f, 3.5f);
		hole2.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole2 = hole2.AddComponent<Rigidbody>();
		rb_hole2.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole3 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole3.transform.position = new Vector3 (-3.5f, 3.5f, -3.5f);
		hole3.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole3 = hole3.AddComponent<Rigidbody>();
		rb_hole3.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole4 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole4.transform.position = new Vector3 (-3.5f, 3.5f, 3.5f);
		hole4.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole4 = hole4.AddComponent<Rigidbody>();
		rb_hole4.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole5 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole5.transform.position = new Vector3 (3.5f, -3.5f, -3.5f);
		hole5.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole5 = hole5.AddComponent<Rigidbody>();
		rb_hole5.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole6 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole6.transform.position = new Vector3 (3.5f, -3.5f, 3.5f);
		hole6.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole6 = hole6.AddComponent<Rigidbody>();
		rb_hole6.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole7 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole7.transform.position = new Vector3 (3.5f, 3.5f, -3.5f);
		hole7.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole7 = hole7.AddComponent<Rigidbody>();
		rb_hole7.constraints = RigidbodyConstraints.FreezeAll;

		GameObject hole8 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole8.transform.position = new Vector3 (3.5f, 3.5f, 3.5f);
		hole8.GetComponent<Renderer>().sharedMaterial = holeMaterial;
		Rigidbody rb_hole8 = hole8.AddComponent<Rigidbody>();
		rb_hole8.constraints = RigidbodyConstraints.FreezeAll;


		//ignore collisions between gaming box objects
		Physics.IgnoreCollision (box1.GetComponent<Collider> (), box2.GetComponent<Collider> ());
		Physics.IgnoreCollision (box1.GetComponent<Collider> (), box3.GetComponent<Collider> ());
		Physics.IgnoreCollision (box1.GetComponent<Collider> (), box4.GetComponent<Collider> ());
		Physics.IgnoreCollision (box1.GetComponent<Collider> (), box5.GetComponent<Collider> ());
		Physics.IgnoreCollision (box1.GetComponent<Collider> (), box6.GetComponent<Collider> ());

		Physics.IgnoreCollision (box2.GetComponent<Collider> (), box3.GetComponent<Collider> ());
		Physics.IgnoreCollision (box2.GetComponent<Collider> (), box4.GetComponent<Collider> ());
		Physics.IgnoreCollision (box2.GetComponent<Collider> (), box5.GetComponent<Collider> ());
		Physics.IgnoreCollision (box2.GetComponent<Collider> (), box6.GetComponent<Collider> ());

		Physics.IgnoreCollision (box3.GetComponent<Collider> (), box4.GetComponent<Collider> ());
		Physics.IgnoreCollision (box3.GetComponent<Collider> (), box5.GetComponent<Collider> ());
		Physics.IgnoreCollision (box3.GetComponent<Collider> (), box6.GetComponent<Collider> ());

		Physics.IgnoreCollision (box4.GetComponent<Collider> (), box5.GetComponent<Collider> ());
		Physics.IgnoreCollision (box4.GetComponent<Collider> (), box6.GetComponent<Collider> ());

		Physics.IgnoreCollision (box5.GetComponent<Collider> (), box6.GetComponent<Collider> ());


		Physics.IgnoreCollision (box1.GetComponent<Collider> (), hole1.GetComponent<Collider> ());
		Physics.IgnoreCollision (box4.GetComponent<Collider> (), hole1.GetComponent<Collider> ());
		Physics.IgnoreCollision (box5.GetComponent<Collider> (), hole1.GetComponent<Collider> ());

		Physics.IgnoreCollision (box2.GetComponent<Collider> (), hole2.GetComponent<Collider> ());
		Physics.IgnoreCollision (box4.GetComponent<Collider> (), hole2.GetComponent<Collider> ());
		Physics.IgnoreCollision (box5.GetComponent<Collider> (), hole2.GetComponent<Collider> ());

		Physics.IgnoreCollision (box1.GetComponent<Collider> (), hole3.GetComponent<Collider> ());
		Physics.IgnoreCollision (box3.GetComponent<Collider> (), hole3.GetComponent<Collider> ());
		Physics.IgnoreCollision (box5.GetComponent<Collider> (), hole3.GetComponent<Collider> ());

		Physics.IgnoreCollision (box2.GetComponent<Collider> (), hole4.GetComponent<Collider> ());
		Physics.IgnoreCollision (box3.GetComponent<Collider> (), hole4.GetComponent<Collider> ());
		Physics.IgnoreCollision (box5.GetComponent<Collider> (), hole4.GetComponent<Collider> ());

		Physics.IgnoreCollision (box1.GetComponent<Collider> (), hole5.GetComponent<Collider> ());
		Physics.IgnoreCollision (box4.GetComponent<Collider> (), hole5.GetComponent<Collider> ());
		Physics.IgnoreCollision (box6.GetComponent<Collider> (), hole5.GetComponent<Collider> ());

		Physics.IgnoreCollision (box2.GetComponent<Collider> (), hole6.GetComponent<Collider> ());
		Physics.IgnoreCollision (box4.GetComponent<Collider> (), hole6.GetComponent<Collider> ());
		Physics.IgnoreCollision (box6.GetComponent<Collider> (), hole6.GetComponent<Collider> ());

		Physics.IgnoreCollision (box1.GetComponent<Collider> (), hole7.GetComponent<Collider> ());
		Physics.IgnoreCollision (box3.GetComponent<Collider> (), hole7.GetComponent<Collider> ());
		Physics.IgnoreCollision (box6.GetComponent<Collider> (), hole7.GetComponent<Collider> ());

		Physics.IgnoreCollision (box2.GetComponent<Collider> (), hole8.GetComponent<Collider> ());
		Physics.IgnoreCollision (box3.GetComponent<Collider> (), hole8.GetComponent<Collider> ());
		Physics.IgnoreCollision (box6.GetComponent<Collider> (), hole8.GetComponent<Collider> ());
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private GameObject createWall(Vector3 scale, Vector3 position, string name) {
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wall.transform.localScale = scale;
		wall.transform.position = position;
		wall.name = name;
		wall.transform.parent = GameObject.Find("GamingBox").transform;
		wall.GetComponent<Renderer>().sharedMaterial = boxMaterial;

		Rigidbody rb = wall.AddComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		wall.AddComponent<BoxCollision> ();

		return wall;
	}

	private GameObject createHole(Vector3 position, string name) {
		GameObject hole = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		hole.transform.position = position;
		hole.name = name;
		hole.transform.parent = GameObject.Find("GamingBox").transform;
		hole.GetComponent<Renderer>().sharedMaterial = holeMaterial;

		return hole;
	}
}

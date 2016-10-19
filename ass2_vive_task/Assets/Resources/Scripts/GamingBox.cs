using UnityEngine;
using System.Collections;

public class GamingBox : MonoBehaviour {
	public Material boxMaterial;
	public Material holeMaterial;

	// Use this for initialization
	void Start () {
		GameObject box1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box1.transform.localScale = new Vector3 (7, 7, 0.1f);
		box1.transform.position = new Vector3(0, 0,-3.5f);
		box1.GetComponent<Renderer>().sharedMaterial = boxMaterial;
		Rigidbody rb = box1.AddComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		box1.AddComponent<BoxCollision> ();

		GameObject box2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box2.transform.localScale = new Vector3 (7, 7, 0.1f);
		box2.transform.position = new Vector3(0, 0, 3.5f);
		box2.GetComponent<Renderer>().sharedMaterial = boxMaterial;
		Rigidbody rb2 = box2.AddComponent<Rigidbody>();
		rb2.constraints = RigidbodyConstraints.FreezeAll;
		box2.AddComponent<BoxCollision> ();

		GameObject box3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box3.transform.localScale = new Vector3 (7, 0.1f, 7);
		box3.transform.position = new Vector3(0, 3.5f, 0);
		box3.GetComponent<Renderer>().sharedMaterial = boxMaterial;
		Rigidbody rb3 = box3.AddComponent<Rigidbody>();
		rb3.constraints = RigidbodyConstraints.FreezeAll;
		box3.AddComponent<BoxCollision> ();

		GameObject box4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box4.transform.localScale = new Vector3 (7, 0.1f, 7);
		box4.transform.position = new Vector3(0, -3.5f, 0);
		box4.GetComponent<Renderer>().sharedMaterial = boxMaterial;
		Rigidbody rb4 = box4.AddComponent<Rigidbody>();
		rb4.constraints = RigidbodyConstraints.FreezeAll;
		box4.AddComponent<BoxCollision> ();

		GameObject box5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box5.transform.localScale = new Vector3 (0.1f, 7, 7);
		box5.transform.position = new Vector3(-3.5f, 0, 0);
		box5.GetComponent<Renderer>().sharedMaterial = boxMaterial;
		Rigidbody rb5 = box5.AddComponent<Rigidbody>();
		rb5.constraints = RigidbodyConstraints.FreezeAll;
		box5.AddComponent<BoxCollision> ();

		GameObject box6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box6.transform.localScale = new Vector3 (0.1f, 7, 7);
		box6.transform.position = new Vector3(3.5f, 0, 0);
		box6.GetComponent<Renderer>().sharedMaterial = boxMaterial;
		Rigidbody rb6 = box6.AddComponent<Rigidbody>();
		rb6.constraints = RigidbodyConstraints.FreezeAll;
		box6.AddComponent<BoxCollision> ();

		//holes
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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

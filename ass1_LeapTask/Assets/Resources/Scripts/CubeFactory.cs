using UnityEngine;
using System.Collections;
using Leap.Unity;

public class CubeFactory : MonoBehaviour {
	
	[SerializeField]
	private PinchDetector _pinchDetectorL;
	public PinchDetector PinchDetectorL {
		get {
			return _pinchDetectorL;
		}
		set {
			_pinchDetectorL = value;
		}
	}

	[SerializeField]
	private PinchDetector _pinchDetectorR;
	public PinchDetector PinchDetectorR {
		get {
			return _pinchDetectorR;
		}
		set {
			_pinchDetectorR = value;
		}
	}

	private GameObject cube;

	private bool cubeCreationRunning = false;
	//private bool lposSet = false;
	//private bool rposSet = false;

	private float distance;
	private Vector3 maxScaleSize = new Vector3(5,5,5);
	private Vector3 minScaleSize = new Vector3(0.5f, 0.5f, 0.5f);
	public float scaleFactor;

	// Use this for initialization
	void Start () {
		/*
		cubeCreationRunning = true;

		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.AddComponent<Rigidbody>();
		cube.transform.position = new Vector3(1, 1, 1);

		cube.GetComponent<Rigidbody> ().useGravity = false;
		*/
	}

	public void StartCubeCreation() {
		cubeCreationRunning = true;

		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.AddComponent<Rigidbody>();
		cube.transform.position = (_pinchDetectorL.Position + _pinchDetectorR.Position) / 2.0f;

		cube.GetComponent<Rigidbody> ().useGravity = false;
	}

	public void EndCubeCreation() {
		cubeCreationRunning = false;
		cube.GetComponent<Rigidbody> ().useGravity = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (cubeCreationRunning == true) {
			if (_pinchDetectorL.DidChangeFromLastFrame || _pinchDetectorR.DidChangeFromLastFrame) {
				float newDistance = Vector3.Distance (_pinchDetectorL.Position, _pinchDetectorR.Position);

				if (newDistance > distance
					&& cube.transform.localScale.x < maxScaleSize.x
					&& cube.transform.localScale.y < maxScaleSize.y
					&& cube.transform.localScale.z < maxScaleSize.z) {
					cube.transform.localScale += Vector3.one * scaleFactor;
				} else if (newDistance < distance
					&& cube.transform.localScale.x > minScaleSize.x
					&& cube.transform.localScale.y > minScaleSize.y
					&& cube.transform.localScale.z > minScaleSize.z) {
					cube.transform.localScale -= Vector3.one * scaleFactor;
				}
				distance = newDistance;
			}

			/*
			//
			//here starts update with mouse
			//
			if (Input.GetKeyDown ("q")) {
				cubeCreationRunning = false;
				cube.GetComponent<Rigidbody> ().useGravity = true;
			}
						 
			Vector3 lpos = new Vector3();
			Vector3 rpos = new Vector3();

			//left click
			if (Input.GetMouseButtonDown (0)) {
				lposSet = true;
				lpos = Input.mousePosition;
			}
			//right click
			if (Input.GetMouseButtonDown (1)) {
				rposSet = true;
				rpos = Input.mousePosition;
			}


			if (lposSet == true && rposSet == true) {
				float newDistance = Vector3.Distance (lpos, rpos);
				Debug.Log ("old distance: " + distance);
				Debug.Log ("new distance: " + newDistance);

				Vector3 scaleFactor = new Vector3 (0.1f, 0.1f, 0.1f);
				if (newDistance > distance
					&& cube.transform.localScale.x < maxScaleSize.x
					&& cube.transform.localScale.y < maxScaleSize.y
					&& cube.transform.localScale.z < maxScaleSize.z) {
					cube.transform.localScale += scaleFactor;
				} else if (newDistance < distance
					&& cube.transform.localScale.x > minScaleSize.x
					&& cube.transform.localScale.y > minScaleSize.y
					&& cube.transform.localScale.z > minScaleSize.z) {
					cube.transform.localScale -= scaleFactor;
				}
				distance = newDistance;

				lposSet = false;
				rposSet = false;
			}
			*/
		}
	}
}

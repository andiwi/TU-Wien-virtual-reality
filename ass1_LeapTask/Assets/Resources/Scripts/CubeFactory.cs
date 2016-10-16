using UnityEngine;
using System.Collections;
using Leap.Unity;

public class CubeFactory : MonoBehaviour {

	public float scaleFactor; //recommended: between 0 and 1
	public float accuracy_threshold; //only distance changes higher than this threshold will result in cube size changes.

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

    [Tooltip("Specify the cube prefab to be produced")]
    public GameObject cubePrefab;

    private GameObject cube;

	private bool cubeCreationRunning = false;
    //private bool lposSet = false;
    //private bool rposSet = false;

    private float distance;
	public Vector3 maxScaleSize = new Vector3(5,5,5);
    public Vector3 minScaleSize = new Vector3(0.05f, 0.05f, 0.05f);

	// Use this for initialization
	void Start () {
		cubeCreationRunning = false;

	}

	public void StartCubeCreation() {
		cubeCreationRunning = true;

        cube = Instantiate(cubePrefab);
		cube.transform.position = (_pinchDetectorL.Position + _pinchDetectorR.Position) / 2f;
        distance = Vector3.Distance(_pinchDetectorL.Position, _pinchDetectorR.Position); 

        cube.transform.localScale = new Vector3(distance/2f, distance/2f, distance/2f);

        //Debug.Log("StartCubeCreation - cube scale: " + cube.transform.localScale);
    }

	public void EndCubeCreation() {
		cubeCreationRunning = false;
		Rigidbody rigid = cube.AddComponent<Rigidbody>();
        rigid.interpolation = RigidbodyInterpolation.Interpolate;

        Leap.Unity.Interaction.InteractionBehaviour behave = cube.AddComponent<Leap.Unity.Interaction.InteractionBehaviour>();
        behave.Manager = GameObject.FindWithTag("InteractionManager").GetComponent< Leap.Unity.Interaction.InteractionManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if (cubeCreationRunning == true) {
			
			float newDistance = Vector3.Distance (_pinchDetectorL.Position, _pinchDetectorR.Position);
			float deltaDistance = (newDistance - distance) * scaleFactor;

            //Debug.Log("newDistance: " + newDistance + ", distance: " + distance + ", deltaDistance: " + deltaDistance);


            if (deltaDistance > accuracy_threshold
				&& cube.transform.localScale.x < maxScaleSize.x
				&& cube.transform.localScale.y < maxScaleSize.y
				&& cube.transform.localScale.z < maxScaleSize.z) {
				cube.transform.localScale += (Vector3.one * deltaDistance);
				distance = newDistance;
			} else if (deltaDistance < -accuracy_threshold
				&& cube.transform.localScale.x > minScaleSize.x
				&& cube.transform.localScale.y > minScaleSize.y
				&& cube.transform.localScale.z > minScaleSize.z) {
				cube.transform.localScale += (Vector3.one * deltaDistance);
				distance = newDistance;
			}
			

			/*
			//
			//here starts update with mouse
			//
			if (Input.GetKeyDown ("q")) {
				cubeCreationRunning = false;
				cube.AddComponent<Rigidbody>();
				//cube.GetComponent<Rigidbody> ().useGravity = true;
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

				float deltaDistance = (newDistance - distance) * scaleFactor;
				Debug.Log (deltaDistance);

				if (deltaDistance > accuracy_threshold
					&& cube.transform.localScale.x < maxScaleSize.x
					&& cube.transform.localScale.y < maxScaleSize.y
					&& cube.transform.localScale.z < maxScaleSize.z) {
						cube.transform.localScale += (Vector3.one * deltaDistance);
						distance = newDistance;
				} else if (deltaDistance < -accuracy_threshold
					&& cube.transform.localScale.x > minScaleSize.x
					&& cube.transform.localScale.y > minScaleSize.y
					&& cube.transform.localScale.z > minScaleSize.z) {
						cube.transform.localScale += (Vector3.one * deltaDistance);
						distance = newDistance;
				}

				lposSet = false;
				rposSet = false;
			}
			*/

		}
	}
}

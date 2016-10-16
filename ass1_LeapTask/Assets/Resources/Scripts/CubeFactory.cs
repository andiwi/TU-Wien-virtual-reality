using UnityEngine;
using System.Collections;
using Leap.Unity;

public class CubeFactory : MonoBehaviour {

    [Tooltip("Scale factor for the scaling - recommended: between 0 and 1")]
    public float scaleFactor;

    [Tooltip("only distance changes higher than this threshold will result in cube size changes.")]
    public float accuracy_threshold;

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

    [Tooltip("Specify the (e.g. cube) prefab to be produced")]
    public GameObject cubePrefab;

    public Vector3 maxScaleSize = new Vector3(5, 5, 5);
    public Vector3 minScaleSize = new Vector3(0.05f, 0.05f, 0.05f);

    private GameObject cube;
	private bool cubeCreationRunning = false;
    private float distance;

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
			
		}
	}
}

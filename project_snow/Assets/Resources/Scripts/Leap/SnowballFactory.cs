using UnityEngine;
using System.Collections;
using Leap.Unity;
using Leap;

public class SnowballFactory : MonoBehaviour {

    [Tooltip("Scale factor for the scaling - recommended: between 0 and 1")]
    public float scaleFactor;

    [Tooltip("only distance changes higher than this threshold will result in cube size changes.")]
    public float accuracy_threshold;

	[SerializeField]
	private ExtendedFingerDetector _extendedFingerDetectorL;
	public ExtendedFingerDetector ExtendedFingerDetectorL {
		get {
			return _extendedFingerDetectorL;
		}
		set {
			_extendedFingerDetectorL = value;
		}
	}

	[SerializeField]
	private ExtendedFingerDetector _extendedFingerDetectorR;
	public ExtendedFingerDetector ExtendedFingerDetectorR {
		get {
			return _extendedFingerDetectorR;
		}
		set {
			_extendedFingerDetectorR = value;
		}
	}

    [Tooltip("Specify the (e.g. snowball) prefab to be produced")]
    public GameObject snowballPrefab;

    public Vector3 maxScaleSize = new Vector3(5, 5, 5);
    public Vector3 minScaleSize = new Vector3(0.05f, 0.05f, 0.05f);

    private GameObject snowball;
    private float handDistance;
	private bool leftHandFist;
	private bool rightHandFist;
	private bool snowballCreationProcess;
	private bool controlSnowballWithLeftHand;
	private bool controlSnowballWithRightHand;

	// Use this for initialization
	void Start () {
	}

	public void LeftHandFingerExtended() {
		//Debug.Log ("LeftHandFingerExtended");
		leftHandFist = false;

		if (controlSnowballWithLeftHand == true) {
			controlSnowballWithLeftHand = false;
			//TODO throw snowball
			// drop snowball
			dropSnowball();
		}
	}

	public void RightHandFingerExtended() {
		//Debug.Log ("RightHandFingerExtended");
		rightHandFist = false;

		if (controlSnowballWithRightHand == true) {
			controlSnowballWithRightHand = false;
			//TODO throw snowball
			// drop snowball
			dropSnowball();
		}
	}

	public void LeftHandFist() {
		//Debug.Log("LeftHandFist()");
		leftHandFist = true;
	}

	public void RightHandFist() {
		//Debug.Log ("RightHandFist()");
		rightHandFist = true;
	}

	public void StartSnowballCreation() {
		if (snowball == null) {
			Debug.Log ("StartSnowballCreation");

			CapsuleHand capsulehandL = GameObject.Find ("CapsuleHand_L").GetComponent<CapsuleHand> ();
			CapsuleHand capsulehandR = GameObject.Find ("CapsuleHand_R").GetComponent<CapsuleHand> ();

			if (capsulehandL != null && capsulehandR != null) {
				Hand handL = capsulehandL.GetLeapHand ();
				Hand handR = capsulehandR.GetLeapHand ();

				if (handL != null && handR != null) {
					handDistance = handL.PalmPosition.DistanceTo (handR.PalmPosition);

					if (handDistance < 0.2) {
						Debug.Log ("Hands are together");
						snowballCreationProcess = true;

						snowball = Instantiate (snowballPrefab);
						snowball.transform.position = (handL.PalmPosition.ToVector3 () + handR.PalmPosition.ToVector3 ()) / 2f;
						snowball.transform.localScale = new Vector3 (handDistance / 2f, handDistance / 2f, handDistance / 2f);
					}
				}
			}
		}
	}

	public void EndSnowballCreation() {
		if(snowball != null) {
			Debug.Log ("EndSnowballCreation");
			snowballCreationProcess = false;

			if (leftHandFist == true && rightHandFist == false) {
				//take snowball with left hand
				controlSnowballWithLeftHand = true;

			} else if (leftHandFist == false && rightHandFist == true) {
				// take snowball with right hand
				controlSnowballWithRightHand = true;

			} else {
				// drop snowball
				dropSnowball();
			}



			//Leap.Unity.Interaction.InteractionBehaviour behave = snowball.AddComponent<Leap.Unity.Interaction.InteractionBehaviour>();
			//behave.Manager = GameObject.FindWithTag("InteractionManager").GetComponent< Leap.Unity.Interaction.InteractionManager>();
		}
    }
	
	// Update is called once per frame
	void Update () {
		if(snowball != null) {
			if (snowballCreationProcess == true) {
				GameObject capsulehandObjL = GameObject.Find ("CapsuleHand_L");
				GameObject capsulehandObjR = GameObject.Find ("CapsuleHand_R");

				if (capsulehandObjL != null && capsulehandObjR != null) {
					
					CapsuleHand capsulehandL = capsulehandObjL.GetComponent<CapsuleHand> ();
					CapsuleHand capsulehandR = capsulehandObjR.GetComponent<CapsuleHand> ();

					if (capsulehandL != null && capsulehandR != null) {
						Hand handL = capsulehandL.GetLeapHand ();
						Hand handR = capsulehandR.GetLeapHand ();

						if (handL != null && handR != null) {
							float newHandDistance = handL.PalmPosition.DistanceTo (handR.PalmPosition);
							float deltaDistance = (newHandDistance - handDistance) * scaleFactor;

							if (deltaDistance > accuracy_threshold
							   && snowball.transform.localScale.x < maxScaleSize.x
							   && snowball.transform.localScale.y < maxScaleSize.y
							   && snowball.transform.localScale.z < maxScaleSize.z) {
								snowball.transform.localScale += (Vector3.one * deltaDistance);
								handDistance = newHandDistance;
							} else if (deltaDistance < -accuracy_threshold
							          && snowball.transform.localScale.x > minScaleSize.x
							          && snowball.transform.localScale.y > minScaleSize.y
							          && snowball.transform.localScale.z > minScaleSize.z) {
								snowball.transform.localScale += (Vector3.one * deltaDistance);
								handDistance = newHandDistance;
							}
						}
					}
				}
			} else if (controlSnowballWithLeftHand == true) {
				GameObject capsulehandObjL = GameObject.Find ("CapsuleHand_L");

				if (capsulehandObjL != null) {
					CapsuleHand capsulehandL = capsulehandObjL.GetComponent<CapsuleHand> ();

					if (capsulehandL != null) {
						Hand handL = capsulehandL.GetLeapHand ();

						if (handL != null) {
							snowball.transform.position = handL.PalmPosition.ToVector3 ();
						}
					}
				}
			} else if (controlSnowballWithRightHand == true) {
				GameObject capsulehandObjR = GameObject.Find ("CapsuleHand_R");

				if (capsulehandObjR != null) {
					CapsuleHand capsulehandR = capsulehandObjR.GetComponent<CapsuleHand> ();

					if (capsulehandR != null) {
						Hand handR = capsulehandR.GetLeapHand ();

						if (handR != null) {
							snowball.transform.position = handR.PalmPosition.ToVector3 ();
						}
					}
				}
			}
		}
	}

	private void dropSnowball() {
		if (snowball != null) {
			Rigidbody rigid = snowball.AddComponent<Rigidbody>();
			rigid.interpolation = RigidbodyInterpolation.Interpolate;

			snowball = null;
		}
	}
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Leap.Unity;
using Leap;

public class SnowballController : NetworkBehaviour {

	[Tooltip("Scale factor for the scaling - recommended: between 0 and 1")]
	public float scaleFactor = 1;

	[Tooltip("only distance changes higher than this threshold will result in cube size changes.")]
	public float accuracy_threshold; //1e-05f

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

	public GameObject snowballContainer;


	public Vector3 maxScaleSize = new Vector3(0.3f, 0.3f, 0.3f);
	public Vector3 minScaleSize = new Vector3(0.05f, 0.05f, 0.05f);
	public float triggerHandDistance = 0.2f;
    public bool isLeap;

	[SyncVar]
	private GameObject snowball;

	private float handDistance;
	private bool leftHandFist;
	private bool rightHandFist;
	private bool snowballCreationProcess;
	private bool controlSnowballWithLeftHand;
	private bool controlSnowballWithRightHand;

    

	void Start () {
		//Debug.Log ("SnowballController start");
        if (isLocalPlayer && isLeap)
		{
			//Debug.Log ("SnowballController start isLocalPlayer");
			LocalSnowballController.Singleton.SetSnowballController(this);
		}

		if (snowballContainer == null) {
            snowballContainer = GameObject.Find("Snowballs");
        }
	}

	public void LeftHandFingerExtended() {
		//Debug.Log ("LeftHandFingerExtended");
        if (isLocalPlayer && isLeap)
        {
			//Debug.Log ("LeftHandFingerExtended: isLocalPlayer");
			leftHandFist = false;

			if (controlSnowballWithLeftHand == true) {
				controlSnowballWithLeftHand = false;
				//throw snowball
				Hand hand = getLeftHand();
				throwSnowball(hand);
			}
		}
	}

	public void RightHandFingerExtended() {
		//Debug.Log ("RightHandFingerExtended");
        if (isLocalPlayer && isLeap)
        {
			rightHandFist = false;

			if (controlSnowballWithRightHand == true) {
				controlSnowballWithRightHand = false;
				//throw snowball
				Hand hand = getRightHand ();
				throwSnowball (hand);
			}
		}
	}

	public void LeftHandFist() {
		//Debug.Log("LeftHandFist()");
        if (isLocalPlayer && isLeap)
        {
			leftHandFist = true;
		}
	}

	public void RightHandFist() {
		//Debug.Log ("RightHandFist()");
        if (isLocalPlayer && isLeap)
        {
			rightHandFist = true;
		}
	}
		
	public void StartSnowballCreation() {
		//Debug.Log ("StartSnowballCreation");

		if (!isLocalPlayer) {
			//Debug.Log ("server return");
			return;
		}

        if (snowball == null && isLeap)
        {
			//Debug.Log ("StartSnowballCreation");
			Hand handL = getLeftHand();
			Hand handR = getRightHand();

			if (handL != null && handR != null) {
				handDistance = handL.PalmPosition.DistanceTo (handR.PalmPosition);

				if (handDistance < triggerHandDistance) {
					//Debug.Log ("Hands are together");
					snowballCreationProcess = true;

					//tell server to create snowball at position and give me authority
					Vector3 position = (handL.PalmPosition.ToVector3 () + handR.PalmPosition.ToVector3 ()) / 2f;
					//Vector3 localScale = new Vector3 (handDistance / 2f, handDistance / 2f, handDistance / 2f);
					Vector3 localScale = new Vector3 (0.4f, 0.4f, 0.4f);

					this.CmdSpawnSnowball(position, localScale);
				}
			}
		}
	}
		
	public void EndSnowballCreation() {
		//Debug.Log ("EndSnowballCreation");
		if (!isLocalPlayer) {
			//Debug.Log ("server return");
			return;
		}

        if (snowball != null && isLeap)
        {
			//Debug.Log ("EndSnowballCreation - snowball != null");
			snowballCreationProcess = false;

			if (leftHandFist == true && rightHandFist == false) {
				//take snowball with left hand
				//Debug.Log("take snowball with left hand");
				controlSnowballWithLeftHand = true;

			} else if (leftHandFist == false && rightHandFist == true) {
				// take snowball with right hand
				controlSnowballWithRightHand = true;

			} else {
				// drop snowball
				dropSnowball();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

        if (snowball != null && isLeap)
        {
			if (snowballCreationProcess == true) {
				//Debug.Log ("scaleSnowball");
				//scaleSnowball (); //TODO scaling over network does not sync
			} else if (controlSnowballWithLeftHand == true) {
				Hand handL = getLeftHand ();

				if (handL != null) {
					snowball.transform.position = handL.PalmPosition.ToVector3 ();
				}
			} else if (controlSnowballWithRightHand == true) {
				Hand handR = getRightHand ();

				if (handR != null) {
					snowball.transform.position = handR.PalmPosition.ToVector3 ();
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

	private void throwSnowball(Hand hand) {
		//Debug.Log ("throwSnowball");
		if (snowball != null) {
			if (hand != null) {
				Vector3 direction = hand.PalmVelocity.ToVector3 ();
				direction = Vector3.Normalize (direction);

				direction.y = 0.3f; //to get a little upwards direction
				direction.z += 0.2f;
				if (direction.z < 0.3f) {
					direction.z = 0.3f;
				}
					
				//limit left right throwing
				if (direction.x < -0.3f) {
					direction.x = -0.3f;
				}
				if (direction.x > 0.3f) {
					direction.x = 0.3f;
				}

				//direction = Vector3.Normalize (direction);

				float force = 1400f;
				Debug.Log (direction * force);
				CmdThrowSnowball (direction * force);
			}
		}
	}

	//does not work with networking
	private void scaleSnowball() {
		Hand handL = getLeftHand();
		Hand handR = getRightHand();

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

	private Hand getLeftHand() {
		GameObject capsulehandObjL = GameObject.Find ("CapsuleHand_L");
		if (capsulehandObjL != null) {
			CapsuleHand capsulehandL = capsulehandObjL.GetComponent<CapsuleHand> ();

			if (capsulehandL != null) {
				return capsulehandL.GetLeapHand ();
			}
		}
		return null;
	}

	private Hand getRightHand() {
		GameObject capsulehandObjR = GameObject.Find ("CapsuleHand_R");
		if (capsulehandObjR != null) {
			CapsuleHand capsulehandR = capsulehandObjR.GetComponent<CapsuleHand> ();

			if (capsulehandR != null) {
				return capsulehandR.GetLeapHand ();
			}
		}
		return null;
	}
		
	[Command]
	public void CmdSpawnSnowball(Vector3 position, Vector3 localScale)
	{
		//Debug.Log ("CmdSpawnSnowball");
		SpawnSnowball (position, localScale);
	}

	[Server]
	public void SpawnSnowball(Vector3 position, Vector3 localScale) {
		GameObject snowball = (GameObject) Instantiate (snowballPrefab, transform);

        //snowballContainer = GameObject.Find("Snowballs");
        snowball.transform.parent = snowballContainer.transform;
		//snowball.transform.parent = this.transform;
		snowball.transform.position = position;
		snowball.transform.localScale = localScale;

        NetworkServer.Spawn(snowball);

        this.snowball = snowball;
		//Debug.Log ("Spawned Snowball");

		NetworkIdentity netID = snowball.GetComponent<NetworkIdentity> ();
		NetworkConnection conn = connectionToClient;

		netID.AssignClientAuthority (conn);
	}

	[Command]
	public void CmdThrowSnowball(Vector3 direction) {
		//Debug.Log ("CmdThrowSnowball()");
		ThrowSnowball (direction);
	}

	[Server]
	public void ThrowSnowball(Vector3 force) {
        //Debug.Log ("ThrowSnowball()");
        //Rigidbody rigid = snowball.AddComponent<Rigidbody>();
        Rigidbody rigid = snowball.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        //force = new Vector3 (0, 300, 500);
        rigid.AddForce (force);

		NetworkIdentity netID = snowball.GetComponent<NetworkIdentity> ();
		Debug.Log (connectionToClient);
		netID.RemoveClientAuthority (connectionToClient);

		snowball = null;
	}
}

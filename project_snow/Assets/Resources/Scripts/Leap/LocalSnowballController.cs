using UnityEngine;
using System.Collections;

public class LocalSnowballController : MonoBehaviour {

	private SnowballController snowballController;

	public static LocalSnowballController Singleton { get; private set; }

	public void SetSnowballController(SnowballController snowballController) {
		this.snowballController = snowballController;
	}

	// Use this for initialization
	void Start () {
	
	}

	private void Awake()
	{
		Singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LeftHandFingerExtended() {
		if (snowballController != null) { //should only be true if isLocalPlayer
			snowballController.LeftHandFingerExtended ();
		}
	}

	public void RightHandFingerExtended() {
		if (snowballController != null) { //should only be true if isLocalPlayer
			snowballController.RightHandFingerExtended ();
		}
	}

	public void LeftHandFist() {
		if (snowballController != null) { //should only be true if isLocalPlayer
			snowballController.LeftHandFist ();
		}
	}

	public void RightHandFist() {
		if (snowballController != null) { //should only be true if isLocalPlayer
			snowballController.RightHandFist ();
		}
	}

	public void StartSnowballCreation() {
		if (snowballController != null) { //should only be true if isLocalPlayer
			snowballController.StartSnowballCreation ();
		}
	}

	public void EndSnowballCreation() {
		if (snowballController != null) { //should only be true if isLocalPlayer
			snowballController.EndSnowballCreation ();
		}
	}
}

using UnityEngine;
using System.Collections;

public class Cue : MonoBehaviour {

    [Tooltip("Specify the cue prefab to be produced")]
    public GameObject cuePrefab;

    private GameObject cue;

    public Vector3 initialPosition = new Vector3(0.2f, -0.1f, -1.8f);
    public Vector3 initialRotation = new Vector3(0, 0, 90);

    // Use this for initialization
    void Start () {
        cue = Instantiate(cuePrefab);
        cue.transform.position = initialPosition;
        cue.transform.Rotate(initialRotation);
        cue.transform.parent = transform;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

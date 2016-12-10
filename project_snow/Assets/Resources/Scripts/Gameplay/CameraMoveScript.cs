using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {


    public Camera serverCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        
   
            gameObject.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
     
    }
}

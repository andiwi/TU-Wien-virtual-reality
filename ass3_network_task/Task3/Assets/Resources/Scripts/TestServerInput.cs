using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestServerInput : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer || isClient)
        {
            return;
        }


        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;


        foreach (GameObject curr in GameObject.FindGameObjectsWithTag("shared"))
        {

            curr.transform.Rotate(0, x, 0);
            curr.transform.Translate(0, 0, z);
        }
    }
}

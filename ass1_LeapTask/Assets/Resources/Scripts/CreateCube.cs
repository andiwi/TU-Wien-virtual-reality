using UnityEngine;
using System.Collections;

public class CreateCube : MonoBehaviour {

    public GameObject gameObject;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            
        }
	}


    public void CreateGameObjectInstance()
    {
        gameObject.transform.position = new Vector3(1, 1, 1);
        Instantiate(gameObject);
        Debug.Log("CreateGameObjectInstance called!");
    }
}

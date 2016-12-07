using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SnowballSpawner : NetworkBehaviour {

	[Tooltip("Specify the (e.g. snowball) prefab to be produced")]
	public GameObject snowballPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Server]
	public void SpawnSnowball(NetworkConnection conn, Vector3 position, Vector3 localScale) {
		GameObject snowball = (GameObject) Instantiate (snowballPrefab, transform);
		snowball.transform.parent = this.transform;
		snowball.transform.position = position;
		snowball.transform.localScale = localScale;
		//TODO add Tag shared to GameObject?
		NetworkServer.Spawn (snowball);
		 
		AuthorityManager am = snowball.GetComponent<AuthorityManager> ();
		am.AssignClientAuthority (conn);
	}
}
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Actor : NetworkBehaviour
{

    public Character character;
    public new Transform transform;

    [SyncVar]
    private string prefabName = "";

    //this part is for object sharing
    //*******************************
    //List<NetworkIdentity> sharedObjects; // shared objects on the server or localActor
    List<AuthorityManager> sharedObjects; // shared objects on the server or localActor
    //*******************************


    protected virtual void Awake()
    {
        transform = base.transform;
        sharedObjects = new List<AuthorityManager>();
    }

    // Use this for initialization
    void Start()
    {

        if (isServer || isLocalPlayer)
        {
            if (isLocalPlayer)
            {
                // Inform the local player about his new character
                LocalPlayerController.Singleton.SetActor(this);
                CmdInitialize(prefabName);
            }

            //this part is for object sharing
            //*******************************

            if (isLocalPlayer)
            {
                Debug.Log("Actor name: " + prefabName + " , is LOCAL PLAYER (host: " + IsHost() + ")");
                // find objects that can be manipulated 
                foreach (GameObject curr in GameObject.FindGameObjectsWithTag("shared"))
                {
                    // assign this Actor to the localActor field of the AuthorityManager component of each shared object
                    AuthorityManager authObj = curr.GetComponent<AuthorityManager>();
                    authObj.AssignActor(this);
                    sharedObjects.Add(authObj);
                }
            }
            else if (isServer)
            {
                Debug.Log("Actor name: " + prefabName + " , is SERVER");
                // find objects that can be manipulated 
                foreach (GameObject curr in GameObject.FindGameObjectsWithTag("shared"))
                {
                    AuthorityManager authObj = curr.GetComponent<AuthorityManager>();
                    sharedObjects.Add(authObj);
                }
            }


            //*******************************

            debugLog("initialized - sharedObjsCount: " + sharedObjects.Count);
        }
        else
        {
            // Initialize on startup
            Initialize(prefabName);
        }

        setupHostOrServer();

    }

    private void setupHostOrServer()
    {
        if (IsHost())
        {
            GameObject playerCtrl = GameObject.FindGameObjectWithTag("leapPlayer");
            if (playerCtrl != null)
            {
                playerCtrl.SetActive(true);
                
            }

            GameObject servCamObj = GameObject.Find("ServerCamera");

            if (servCamObj != null)
            {
                Camera camera = servCamObj.GetComponent<Camera>();
                camera.enabled = false;
            }
            else
            {
                Debug.Log("ServerCamera not found :/");
            }

        }
        else if (isServer)
        {
            GameObject playerCtrl = GameObject.FindGameObjectWithTag("leapPlayer");
            if (playerCtrl != null)
            {
                playerCtrl.SetActive(false);
                Debug.Log("deactivate playerCtrl because isServer");
            }
        }
    }

    public bool IsHost()
    {
        return isServer && isClient;
    }

    private void debugLog(string msg)
    {
        Debug.Log("Actor-" + prefabName + ":" + msg);
    }

    public void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        //DEBUG TODO remove
        if (Input.GetKeyDown(KeyCode.Space))
        {
            debugLog("Pressed space bar");

            //test grab first object
            sharedObjects[0].GrabObject();
            //NetworkIdentity test1 = sharedObjects[0].GetNetworkIdentity();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            debugLog("Pressed F");

            sharedObjects[0].UnGrabObject();
        }
    }

    /// <summary>
    /// Updates the actor position and rotation.
    /// This function should be called only by the <see cref="LocalPlayerController"/>.
    /// </summary>
    public void UpdateActorLeft(Vector3 leftPos, Quaternion leftRot) //runs only on LocalPlayer!
    {
        if (character != null)
        {
            character.UpdateCharacterLeft(leftPos, leftRot);
        }
    }

    /// <summary>
    /// Updates the actor position and rotation.
    /// This function should be called only by the <see cref="PlayerController"/>.
    /// </summary>
    public void UpdateActorRight(Vector3 rightPos, Quaternion rightRot) //runs only on LocalPlayer!
    {
        if (character != null)
        {
            character.UpdateCharacterRight(rightPos, rightRot);
        }
    }

    public void SetRightCharacterActive(bool active)
    {
        character.SetRightActive(active);
    }

    public void SetLeftCharacterActive(bool active)
    {
        character.SetLeftActive(active);
    }

    /// <summary>
    /// Initialize the player locally.
    /// </summary>
    /// <param name="prefab">Prefab character name.</param>
    public void Initialize(string prefab)
    {
        prefabName = prefab;
        name = name.Replace("(Clone)", "");

    }

    /// <summary>
    /// Spawns the character of actor on all clients.
    /// This runs on server only.
    /// </summary>
    /// <param name="prefab">Prefab name of the character.</param>
    private void SpawnCharacter(string prefab)
    {
        // Spawn character
        GameObject modelPrefab = Resources.Load("Prefabs/" + prefab) as GameObject;
        GameObject model = (GameObject)Instantiate(modelPrefab, transform.position, transform.rotation) as GameObject;
        NetworkServer.Spawn(model);

        // Attach character to player
        AttachCharacter(model.GetComponent<Character>());
    }

    /// <summary>
    /// Initializes the character on server to inform all clients. 
    /// This command calls the Initialize() method and spawns the character.
    /// </summary>
    [Command]
    public void CmdInitialize(string prefab)
    {
        if (prefab.Length > 0)
        {
            CreateCharacter(prefab);
        }
    }

    /// <summary>
    /// Creates the character and initializes on server.
    /// </summary>
    /// <param name="prefab">The character prefab name.</param>
    [Server]
    public void CreateCharacter(string prefab)
    {
        SpawnCharacter(prefab);
        Initialize(prefab);
    }

    /// <summary>
    /// Main routine to attach the character to this actor
    /// This runs only on Server.
    /// </summary>
    /// <param name="newCharacter">New character to attach.</param>
    [Server]
    public void AttachCharacter(Character newCharacter)
    {
        newCharacter.AttachToActor(netId);
    }


    //this part is for object sharing
    // fill in the implementation
    //*******************************

    // should only be run on localPlayer (by the AuthorityManager of a shared object)
    // ask the server for the authority over an object with NetworkIdentity netID
    public void RequestObjectAuthority(NetworkIdentity netID)
    {
        debugLog("RequestObjectAuthority...");
        CmdAssignObjectAuthorityToClient(netID);
    }

    // should only be run on localPlayer (by the AuthorityManager of a shared object)
    // ask the server to remove the authority over an object with NetworkIdentity netID
    public void ReturnObjectAuthority(NetworkIdentity netID)
    {
        debugLog("ReturnObjectAuthority...");

        //if (IsHost() == false)
        //{
        CmdRemoveObjectAuthorityFromClient(netID);
        //} else
        //{
        //debugLog("client is HOST -> abort return object authority");
        //}
       
    }


    // run on the server
    // netID is NetworkIdentity of a shared object the authority if which should be passed to the client
    [Command]
    void CmdAssignObjectAuthorityToClient(NetworkIdentity netID)
    {
        debugLog("CmdAssignObjectAuthorityToClient received, " + netID);

        AuthorityManager authMan = sharedObjects.Find(curr => curr.GetNetworkIdentity().Equals(netID));
        if (authMan != null)
            authMan.AssignClientAuthority(connectionToClient);
    }

    // run on the server
    // netID is NetworkIdentity of a shared object the authority if which should be removed from the client
    [Command]
    void CmdRemoveObjectAuthorityFromClient(NetworkIdentity netID)
    {
        debugLog("CmdRemoveObjectAuthorityFromClient received");

        AuthorityManager authMan = sharedObjects.Find(curr => curr.GetNetworkIdentity().Equals(netID));
        if (authMan != null)
            authMan.RemoveClientAuthority(connectionToClient);
    }
    //*******************************
}

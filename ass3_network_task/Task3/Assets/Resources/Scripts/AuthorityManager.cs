using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


// TODO: this script should manage authority for a shared object
public class AuthorityManager : NetworkBehaviour
{


    NetworkIdentity netID; // NetworkIdentity component attached to this game object

    // these variables should be set up on a client
    //**************************************************************************************************
    Actor localActor; // Actor that is steering this player 

    private bool grabbed = false; // if this is true client authority for the object should be requested
    public bool grabbedByPlayer // private "grabbed" field can be accessed from other scripts through grabbedByPlayer
    {
        get { return grabbed; }
        set { grabbed = value; }
    }

    OnGrabbedBehaviour onb; // component defining the behaviour of this GO when it is grabbed by a player
                            // this component can implement different functionality for different GO´s


    //***************************************************************************************************

    // these variables should be set up on the server

    // TODO: implement a mechanism for storing consequent authority requests from different clients
    // e.g. manage a situation where a client requests authority over an object that is currently being manipulated by another client

    System.Collections.Generic.Queue<NetworkConnection> authRequestConnections;

    //*****************************************************************************************************

    // TODO: avoid sending two or more consecutive RemoveClientAuthority or AssignClientAUthority commands for the same client and shared object
    // a mechanism preventing such situations can be implemented either on the client or on the server

    // Use this for initialization
    void Start()
    {
        netID = gameObject.GetComponent<NetworkIdentity>();
        onb = gameObject.AddComponent<OnGrabbedBehaviour>();
        authRequestConnections = new System.Collections.Generic.Queue<NetworkConnection>();

        debugLog("initialized AuthorityManager!");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public NetworkIdentity GetNetworkIdentity()
    {
        return netID;
    }

    // assign localActor here
    public void AssignActor(Actor actor)
    {
        localActor = actor;
    }

    private bool hasConnectionAuthority(NetworkConnection con)
    {
        return con.Equals(netID.clientAuthorityOwner);
    }

    // should only be called on server (by an Actor)
    // assign the authority over this game object to a client with NetworkConnection conn
    [Server]
    public void AssignClientAuthority(NetworkConnection conn)
    {
        

        debugLog("AssignClientAuthority...");
        if (netID.localPlayerAuthority
            && hasConnectionAuthority(conn) == false
            && authRequestConnections.Contains(conn) == false)
        {
            debugLog("localPlayerAuthority already granted & not in Queue -> adding to Queue.");
            authRequestConnections.Enqueue(conn);
        }
        else if (netID.localPlayerAuthority == false)
        {
            netID.localPlayerAuthority = true; //TODO why??
            netID.AssignClientAuthority(conn);
            debugLog("granting localPlayerAuthority!");
        }
        else
        {
            debugLog("localPlayerAuth already granted -> authRequest dismissed");
        }

    }

    // should only be called on server (by an Actor)
    // remove the authority over this game object from a client with NetworkConnection conn
    [Server]
    public void RemoveClientAuthority(NetworkConnection conn)
    {

        debugLog("RemoveClientAuthority...");
        if (hasConnectionAuthority(conn) == false)
        {
            debugLog("has no connection authority to remove..");
            return;
        }


        netID.localPlayerAuthority = false;
        netID.RemoveClientAuthority(conn);

        if (authRequestConnections.Count > 0)
        {
            Debug.Log("authRequest queue is not empty, assign auth on first..");
            AssignClientAuthority(authRequestConnections.Dequeue());
        }
    }

    private void debugLog(string msg)
    {
        //Debug.Log("Log - actor: " + prefabName + " isServer: " + isServer + " isLocalPlayer: " + isLocalPlayer + " Msg: " + msg);
        if (isServer)
        {
            Debug.Log("AuthManager, netId: " + netID + "; SERVER: " + msg);
        }
        else
        {
            Debug.Log("AuthManager, netId: " + netID + "; CLIENT: " + msg);
        }

    }


    public void GrabObject()
    {

        localActor.RequestObjectAuthority(netID);
    }

    public void UnGrabObject()
    {
        localActor.ReturnObjectAuthority(netID);
    }

}

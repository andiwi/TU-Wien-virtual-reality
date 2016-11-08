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



    //these variables are set up and handled by both client and server
    Rigidbody rigidbody;


    //***************************************************************************************************

    // these variables should be set up on the server

    // TODO: implement a mechanism for storing consequent authority requests from different clients
    // e.g. manage a situation where a client requests authority over an object that is currently being manipulated by another client

    /// <summary>
    /// don't dare to set this var on the client
    /// </summary>
    bool authorityAssigned;
    System.Collections.Generic.Queue<NetworkConnection> authRequestConnections;

    //*****************************************************************************************************

    // TODO: avoid sending two or more consecutive RemoveClientAuthority or AssignClientAUthority commands for the same client and shared object
    // a mechanism preventing such situations can be implemented either on the client or on the server

    // Use this for initialization
    void Start()
    {
        netID = gameObject.GetComponent<NetworkIdentity>();
        onb = gameObject.GetComponent<OnGrabbedBehaviour>();
        authRequestConnections = new System.Collections.Generic.Queue<NetworkConnection>();
        rigidbody = gameObject.GetComponent<Rigidbody>();

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

    [ClientRpc]
    public void RpcOnAuthorityAssignedToClient()
    {
        if (hasAuthority)
        {
            debugLog("RpcGrabObject...");
            onb.OnGrabbed();
        }
        rigidbody.isKinematic = true;
    }
    [ClientRpc]
    public void RpcOnAuthorityReleasedFromClient()
    {
        if (hasAuthority)
        {
            debugLog("RpcReleaseObject...");
            onb.OnReleased();
        }
        rigidbody.isKinematic = false;
    }

    /// <summary>
    /// callback to tell the greedy client that he is allowed to make a request again
    /// </summary>
    [ClientRpc]
    public void RpcOnRequestProcessed()
    {
        requestSent = false;
    }

    // should only be called on server (by an Actor)
    // assign the authority over this game object to a client with NetworkConnection conn
    [Server]
    public void AssignClientAuthority(NetworkConnection conn)
    {
        //bool host parameter

        debugLog("AssignClientAuthority..." + conn);
        if (authRequestQueueConditionCheck(conn))
        {
            debugLog("localPlayerAuthority already granted & not in Queue -> adding to Queue.");
            authRequestConnections.Enqueue(conn);
        }
        else if (authorityAssigned == false)
        {
            assignAuthority(conn);
        }
        else
        {
            debugLog("localPlayerAuth already granted -> authRequest dismissed");
        }

        RpcOnRequestProcessed();
    }

    [Server]
    private bool authRequestQueueConditionCheck(NetworkConnection conn)
    {
        return authorityAssigned
                    && hasConnectionAuthority(conn) == false
                    && authRequestConnections.Contains(conn) == false;
    }

    [Server]
    private void assignAuthority(NetworkConnection conn)
    {
        authorityAssigned = true;
        netID.AssignClientAuthority(conn);
        debugLog("granting localPlayerAuthority!");
        RpcOnAuthorityAssignedToClient();
        rigidbody.isKinematic = true;

        //conn.
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

        //in case isLocalPlayer -> host -> don't remove client authority, since not possible

        RpcOnAuthorityReleasedFromClient();
        authorityAssigned = false;
        rigidbody.isKinematic = false;

        bool removed = netID.RemoveClientAuthority(conn);
        if (removed)
        {

            if (authRequestConnections.Count > 0)
            {
                Debug.Log("authRequest queue is not empty, assign auth on first..");
                AssignClientAuthority(authRequestConnections.Dequeue());
            }
        }
        else
        {
            Debug.Log("ERROR removeClientAuthority false");
        }

        RpcOnRequestProcessed();
    }

    private void debugLog(string msg)
    {
        //Debug.Log("Log - actor: " + prefabName + " isServer: " + isServer + " isLocalPlayer: " + isLocalPlayer + " Msg: " + msg);
        Debug.Log("AuthManager-" + gameObject.name + ": " + msg);
    }

    private bool requestSent = false;

    [Client]
    public void GrabObject()
    {
        debugLog("GrabObject ... onbIsgrabbed: " + onb.IsGrabbed() + " ; requestSent: " + requestSent);
        if (onb.IsGrabbed() == false && requestSent == false)
            localActor.RequestObjectAuthority(netID);
    }

    [Client]
    public void UnGrabObject()
    {
        debugLog("UnGrabObject ... onbIsgrabbed: " + onb.IsGrabbed() + " ; requestSent: " + requestSent);
        if (onb.IsGrabbed() == true && requestSent == false)
            localActor.ReturnObjectAuthority(netID);
    }

}

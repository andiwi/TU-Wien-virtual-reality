using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

// TODO: this script should manage authority for a shared object
public class AuthorityManager : NetworkBehaviour
{


    NetworkIdentity netIDgameObj; // NetworkIdentity component attached to this game object

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
        netIDgameObj = gameObject.GetComponent<NetworkIdentity>();
        onb = gameObject.GetComponent<OnGrabbedBehaviour>();
        authRequestConnections = new System.Collections.Generic.Queue<NetworkConnection>();
        rigidbody = gameObject.GetComponent<Rigidbody>();

        if (isClient)
        {
            GameManager gameMan = GameManager.Instance;
            if (gameMan && gameMan.localActor)
            {
                localActor = gameMan.localActor;
            }
        }

        debugLog("initialized AuthorityManager!");

    }


    /// <summary>
    ///    assign localActor here 
    /// </summary>
    public void AssignActor(Actor actor)
    {
        localActor = actor;
    }

    private bool hasConnectionAuthority(NetworkConnection con)
    {
        return con.Equals(netIDgameObj.clientAuthorityOwner);
    }

    /// <summary>
    /// all clients callback
    /// </summary>
    [ClientRpc]
    public void RpcOnAuthorityAssignedToClient()
    {
        //if (hasAuthority)
        //{
        //    debugLog("RpcGrabObject...");
        //    onb.OnGrabbed(localActor.gameObject.transform);
        //    grabbed = true;
        //    //EventManager.OnAuthorityAssigned();
        //}
        rigidbody.isKinematic = true;

    }

    [TargetRpc]
    public void TargetRpcOnAuthorityAssigned(NetworkConnection target)
    {
        //no hasAuthority abfrage since it might be delayed
        debugLog("TargetRpcOnAuthorityAssigned...");
        onb.OnGrabbed(getLocalActor().gameObject.transform);
        grabbed = true;
        //rigidbody.isKinematic = true;  //TODO check if isKimenatic is necessary
    }

    [TargetRpc]
    public void TargetRpcOnAuthorityReleased(NetworkConnection target)
    {
        //no hasAuthority abfrage since it might be delayed
        debugLog("TargetRpcOnAuthorityAssigned...");
        onb.OnReleased();
        grabbed = false;
        //rigidbody.isKinematic = false;  //TODO check if isKimenatic is necessary
    }

    [ClientRpc]
    public void RpcOnAuthorityReleasedFromClient()
    {
        //if (hasAuthority)
        //{
        //    debugLog("RpcReleaseObject...");
        //    onb.OnReleased();
        //}
        rigidbody.isKinematic = false;
        //grabbed = false;
    }

    /// <summary>
    /// callback to tell the greedy client that he is allowed to make a request again
    /// </summary>
    [TargetRpc]
    public void TargetRpcOnRequestProcessed(NetworkConnection target)
    {
        requestSent = false;
    }

    // should only be called on server (by an Actor)
    // assign the authority over this game object to a client with NetworkConnection conn
    [Server]
    public void AssignClientAuthority(NetworkConnection conn)
    {

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

        TargetRpcOnRequestProcessed(conn);
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
        netIDgameObj.AssignClientAuthority(conn);
        debugLog("granting localPlayerAuthority!");
        TargetRpcOnAuthorityAssigned(conn);
        RpcOnAuthorityAssignedToClient();
        rigidbody.isKinematic = true;

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

        bool removed = netIDgameObj.RemoveClientAuthority(conn);
        if (removed)
        {
            TargetRpcOnAuthorityReleased(conn);

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

        TargetRpcOnRequestProcessed(conn);
    }

    void OnNetworkDestroy()
    {
        debugLog("OnNetworkDestroy called");
    }

    private void debugLog(string msg)
    {
        //Debug.Log("Log - actor: " + prefabName + " isServer: " + isServer + " isLocalPlayer: " + isLocalPlayer + " Msg: " + msg);
        Debug.Log("AuthManager-" + gameObject.name + ": " + msg);
    }

    private bool requestSent = false;

    [Client]
    private Actor getLocalActor()
    {
        if (localActor == null)
        {
            localActor = GameManager.Instance.localActor;
            return localActor;
        }
        else
        {
            return localActor;
        }
    }

    [Client]
    public void GrabObject()
    {
        //  debugLog("GrabObject ... onbIsgrabbed: " + onb.IsGrabbed() + " ; requestSent: " + requestSent);
        if (grabbed == false)
        {

            if (hasAuthority)
            {

                debugLog("GrabObject - client already has authority -> grab object!");
                onb.OnGrabbed(getLocalActor().gameObject.transform);

            }
            else if (requestSent == false)
            {
                debugLog("GrabObject - client has no authority -> request authority");

                getLocalActor().RequestObjectAuthority(netIDgameObj);
                requestSent = true;
            }
        }
    }


    //[Client]
    //public void GrabObject()
    //{
    //    //  debugLog("GrabObject ... onbIsgrabbed: " + onb.IsGrabbed() + " ; requestSent: " + requestSent);

    //    if (grabbed == false && requestSent == false)
    //    {
    //        localActor.RequestObjectAuthority(netIDgameObj);
    //    }

    //    //task3
    //    //if (onb.IsGrabbed() == false && requestSent == false)
    //    //    localActor.RequestObjectAuthority(netID);
    //}

    /// <summary>
    /// for debugging - keep authority when throwing for now 
    /// </summary>
    [Client]
    public void UnGrabObjectButKeepAuthority()
    {
        debugLog("UnGrabObjectButKeepAuthority - isGrabbed? " + grabbed);

        if (grabbed == true)
        {
            onb.OnReleased();
            grabbed = false;
        }
    }

    [Client]
    public void UnGrabObject()
    {
        // debugLog("UnGrabObject ... onbIsgrabbed: " + onb.IsGrabbed() + " ; requestSent: " + requestSent);

        if (grabbed == true && requestSent == false)
        {
            getLocalActor().ReturnObjectAuthority(netIDgameObj);
            requestSent = true;
        }


        //Task3
        //if (onb.IsGrabbed() == true && requestSent == false)
        //    localActor.ReturnObjectAuthority(netID);
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("OnStartLocalPlayer() ");
        List<PlayerController> playerCtrls = NetworkController.FindInstance().client.connection.playerControllers;

        Debug.Log("playerCtrls: " + playerCtrls.Count + " pls " + playerCtrls);
    }

}

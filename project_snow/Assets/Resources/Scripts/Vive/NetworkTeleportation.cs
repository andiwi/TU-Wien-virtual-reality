using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkTeleportation : NetworkBehaviour
{
    public static NetworkTeleportation Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //called by vive client
    [Client]
    public void initTeleportation(Vector3 newPosition)
    {
        print("initTeleportation (" + newPosition + ") -> send command");
        CmdTeleport(newPosition);
    }

    //received by server
    [Command]
    public void CmdTeleport(Vector3 newPosition)
    {
        //List<GameObject> players = GameManager.Instance.GetPlayers();
        //GameObject leapPlayer = players.Find(curr => "LeapPlayer".Equals(curr.name));

        RpcNotifyTeleportation(newPosition);

        /*
         *        AuthorityManager authMan = sharedObjects.Find(curr => curr.GetNetworkIdentity().Equals(netID));
        if (authMan != null)
            authMan.AssignClientAuthority(connectionToClient);
            */
    }

    //received by clients, further processed by clients with leapController
    [ClientRpc]
    private void RpcNotifyTeleportation(Vector3 newPosition)
    {
        GameObject leapController = GameObject.Find("LeapPlayerController");

        if (leapController)
        {
            print("NetworkTeleportation - client has leapController: do teleportation!");
            leapController.transform.position = newPosition;
        }
        else
        {
            print("NetworkTeleportation - client has no leapController, ignore teleportation");
        }
    }
}

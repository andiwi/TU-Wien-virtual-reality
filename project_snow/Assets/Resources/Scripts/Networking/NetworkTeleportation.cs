using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkTeleportation : NetworkBehaviour
{
    public static NetworkTeleportation Singleton { get; private set; }

    public Vector3 leapPlayerOffset = new Vector3(1, 1.296f, -0.322f);

    void Awake()
    {
        Singleton = this;
    }

    /// <summary>
    ///     called by vive client with new position to teleport to
    /// </summary>
    [Client]
    public void initTeleportation(Vector3 newPosition)
    {
        print("initTeleportation (" + newPosition + ") -> send command");
        CmdTeleport(newPosition);
    }

    //received by server
    [Command]
    private void CmdTeleport(Vector3 newPosition)
    {
        //List<GameObject> players = GameManager.Instance.GetPlayers();
        //GameObject leapPlayer = players.Find(curr => "LeapPlayer".Equals(curr.name));

        RpcNotifyTeleportation(newPosition);
    }

    //received by clients, further processed by clients with leapController
    [ClientRpc]
    private void RpcNotifyTeleportation(Vector3 newPosition)
    {
        GameObject leapController = GameObject.Find("LeapPlayerController");

        if (leapController)
        {
            print("NetworkTeleportation - client has active leapController: do teleportation!");
            leapController.transform.position = newPosition + leapPlayerOffset; //a offset to Vive player - experiment this
           
        }
        else
        {
            print("NetworkTeleportation - client has no active leapController, ignore teleportation");
        }
    }
}

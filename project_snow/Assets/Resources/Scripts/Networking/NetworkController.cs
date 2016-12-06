using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NetworkController : NetworkManager
{

    public bool host;
    public bool server;
    public Camera serverCamera;

    public static NetworkController FindInstance()
    {
        return FindObjectOfType<NetworkController>();
    }

    public class NetMsgType
    {
        public const short EmptyMessage = MsgType.Highest + 1; // Empty network message

        public class EmptyMessageMsg : MessageBase { }
    }

    /// <summary>
    /// Connect to server or create host server.
    /// </summary>
    private void Start()
    {

        //TODO commented out for NetworkManagerHUD control

        //if(server)
        //{
        //    StartServer();
        //}
        //else if(host)
        //{
        //    StartHost();
        //}
        //else
        //{
        //    StartClient();
        //}

    }

    // overriden functions implement only base functionality; however, additional functionality can be implemented here
    public override void OnStartServer()
    {
        Debug.Log("OnStartServer()");
        base.OnStartServer();

        setServerCameraEnabled(true);

    }

    public override void OnStartHost()
    {
        Debug.Log("OnStartHost()");
        host = true;
        base.OnStartHost();

    }

    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("OnStartClient()");     
        base.OnStartClient(client);

        setServerCameraEnabled(false);
    }

    private void setServerCameraEnabled(bool enabled)
    {

        if (serverCamera != null)
        {
            serverCamera.enabled = enabled;

        }
        else
        {
            Debug.Log("setServerCameraEnabled - no serverCamera defined!");
        }
    }
}


﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NetworkController : NetworkManager
{

    public bool host;
    public bool server;

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

        //for spawning the box when server starts (debug)
        //GameObject modelPrefab = Resources.Load("Prefabs/Box") as GameObject;
        //GameObject model = (GameObject)Instantiate(modelPrefab, new Vector3(0.252f,1.037f,-0.17f), transform.rotation) as GameObject;
        //NetworkServer.Spawn(model);

        setupHostOrServer();

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

    }

    /// <summary>
    /// deactivates or activates PlayerController if host/server and ServerCamera
    /// </summary>
    private void setupHostOrServer()
    {
        if (host)
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
        else
        {
            GameObject playerCtrl = GameObject.FindGameObjectWithTag("leapPlayer");
            if (playerCtrl != null)
            {
                playerCtrl.SetActive(false);
                Debug.Log("deactivate playerCtrl because isServer");
            }
        }
    }


}


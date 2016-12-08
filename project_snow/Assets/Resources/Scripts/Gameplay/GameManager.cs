using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Game Manager 
/// </summary>
public class GameManager : NetworkBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;

    }

    GameObject[] players;

    public Actor localActor { get; set; }

    //public GameObject[] players { get; private set; }

    void Start()
    {

        Debug.Log("GameManager startet: isClient: " + isClient+ " isServer: " + isServer  +" isLocalPalyer:" +isLocalPlayer );

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public GameObject[] GetPlayers()
    {
        return players;
    }

    public void CmdOnPlayerConnectedCallback()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Player connected; No of players: " + players.Length);
    }

}


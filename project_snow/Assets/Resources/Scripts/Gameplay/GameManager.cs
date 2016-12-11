using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Game Manager 
/// </summary>
public class GameManager : NetworkBehaviour
{
    [SyncVar]
    private int playerLife = 100;

    [SyncVar]
    private int enemiesAlive = 0;

    private GUIText gameStatus;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;

    }

    //ONLY SET ON CLIENTS
    public Actor localActor { get; set; }


    //ONLY SET ON SERVER 
    //[SyncVar]
    private List<GameObject> players;

    void Start()
    {

        Debug.Log("GameManager startet: isClient: " + isClient + " isServer: " + isServer + " isLocalPalyer:" + isLocalPlayer);

        players = new List<GameObject>();

        gameStatus = this.GetComponentInChildren<GUIText>();
        if (gameStatus)
        {
            gameStatus.text = "Life: " + playerLife;
        }
    }

    public List<GameObject> GetPlayers()
    {
        return players;
    }

    [Server]
    public void AddPlayer(GameObject player)
    {
        players.Add(player);
        print("GameManager.AddPlayer - playerCount:" + players.Count);
    }

    public void HitByEnemySnowball()
    {
        if (!isServer)
        {
            return;
        }

        updateGameStatus(playerLife, enemiesAlive);
        RpcUpdateStatus(playerLife, enemiesAlive);
    }

    private void gameOver()
    {
        gameStatus.text = "GAME OVER!";
    }

    private void gameWin()
    {
        gameStatus.text = "YOU WON!";
    }

    private void updateGameStatus(int playerLife, int enemiesAlive)
    {
        if (enemiesAlive == 0)
        {
            gameWin();
        }
        else
        {
            if (playerLife > 0)
            {
                gameStatus.text = "Life: " + playerLife;
            }
            else
            {
                gameOver();
            }
        }
    }

    [ClientRpc]
    public void RpcUpdateStatus(int playerLife, int enemiesAlive)
    {
        updateGameStatus(playerLife, enemiesAlive);
    }

    public void IncreaseEnemiesAlive()
    {
        if (!isServer)
        {
            return;
        }
        enemiesAlive++;
    }

    public void DecreaseEnemiesAlive()
    {
        if (!isServer)
        {
            return;
        }
        enemiesAlive--;
        updateGameStatus(playerLife, enemiesAlive);
        RpcUpdateStatus(playerLife, enemiesAlive);
    }
}


﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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

    GameObject[] players;

    public Actor localActor { get; set; }

    //public GameObject[] players { get; private set; }

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
		gameStatus = this.GetComponentInChildren<GUIText> ();
		gameStatus.text = "Life: " + playerLife;
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

	public void HitByEnemySnowball() {
		if (!isServer) {
			return;
		}

		updateGameStatus (playerLife, enemiesAlive);
		RpcUpdateStatus (playerLife, enemiesAlive);
	}

	private void gameOver() {
		gameStatus.text = "GAME OVER!";
	}

	private void gameWin() {
		gameStatus.text = "YOU WON!";
	}

	private void updateGameStatus(int playerLife, int enemiesAlive) {
		if (enemiesAlive == 0) {
			gameWin ();
		} else {
			if (playerLife > 0) {
				gameStatus.text = "Life: " + playerLife;
			} else {
				gameOver ();
			}
		}
	}

	[ClientRpc]
	public void RpcUpdateStatus(int playerLife, int enemiesAlive) {
		updateGameStatus (playerLife, enemiesAlive);
	}

	public void IncreaseEnemiesAlive() {
		if (!isServer) {
			return;
		}
		enemiesAlive++;
	}

	public void DecreaseEnemiesAlive() {
		if (!isServer) {
			return;
		}
		enemiesAlive--;
		updateGameStatus (playerLife, enemiesAlive);
		RpcUpdateStatus (playerLife, enemiesAlive);
	}
}


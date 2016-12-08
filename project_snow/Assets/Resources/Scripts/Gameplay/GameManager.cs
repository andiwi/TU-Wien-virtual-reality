using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Game Manager 
/// </summary>
public class GameManager : NetworkBehaviour
{
	private int playerLife = 100;
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

		playerLife -= 10;
		if (playerLife > 0) {
			gameStatus.text = "Life: " + playerLife;
		} else {
			GameOver ();
		}
	}

	public void GameOver() {
		if (!isServer) {
			return;
		}

		gameStatus.text = "GAME OVER!";
	}

}


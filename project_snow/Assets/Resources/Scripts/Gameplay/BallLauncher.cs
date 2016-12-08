using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// taken and changed from https://github.com/SebLague/Kinematic-Equation-Problems
/// </summary>
public class BallLauncher : NetworkBehaviour
{

    float timer;
    public int waitingTime = 1;
    public int ballLivingTime = 10;


    public GameObject snowballPrefab;
    public Transform targetPassed;

    public float height = 25;
    public float heightVariation = 10;
    public float gravity = -18;

    public float targetPosVariation = 4;


    void Update()
    {
        if (isServer == false) { return; }

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            //Action
      
            GameObject targetPlayer = SelectPlayer();
            if (targetPlayer != null)
            {
                GameObject ball = Instantiate(snowballPrefab, transform.position, Quaternion.identity) as GameObject;
                Debug.Log("instantiated ball: " +ball.name);
                Launch(ball, targetPlayer.transform);
            }

            timer = 0;
        }

    }



    [Server]
    private GameObject SelectPlayer()
    {

        //GameObject[] players = GameManager.Instance.GetPlayers();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //TODO rework or just use one target anyways

        if (players == null || players.Length == 0) return null;
        int randomIndex = Random.Range(0, players.Length);

        //Debug.Log("SelectPlayer: " + players.Length + " , sel index:" + randomIndex);

        GameObject targetPlayer = players[randomIndex];
        return targetPlayer;
    }

    [Server]
    void Launch(GameObject ball, Transform target)
    {

        Physics.gravity = Vector3.up * gravity;
        Rigidbody ballRigid = ball.GetComponent<Rigidbody>();
        ballRigid.useGravity = true;
        ballRigid.velocity = CalculateLaunchData(ballRigid, target).initialVelocity;

        NetworkServer.Spawn(ball);
        RpcInitBallAuthManClients(ball);

        StartCoroutine(WaitAndDestroyBall(ball));
    }

    private IEnumerator WaitAndDestroyBall(GameObject ball)
    {
        yield return new WaitForSeconds(ballLivingTime);
        NetworkServer.Destroy(ball);
        Destroy(ball);
        //Debug.Log("waited and destroyed ball " + ball.name); 
    }

    [ClientRpc]
    public void RpcInitBallAuthManClients(GameObject ball)
    {
        AuthorityManager ballAuthMan = ball.GetComponent<AuthorityManager>();
        if (ballAuthMan == null)
        {
            Debug.Log("ERROR RpcInitBallAuthManClients - ballAuthMan == null");
            return;
        }
        Actor clientActor = GameManager.Instance.localActor;
        //Debug.Log("RpcInitBallAuthManClients - assigned localActor: " + clientActor);
        ballAuthMan.AssignActor(clientActor);
    }


    LaunchData CalculateLaunchData(Rigidbody ball, Transform target)
    {
        //height variation
        float h = Random.Range(height - heightVariation, height + heightVariation);

        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        //position variation 
        displacementXZ += new Vector3(Random.Range(-targetPosVariation, targetPosVariation), Random.Range(-targetPosVariation, targetPosVariation));

        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }


    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}

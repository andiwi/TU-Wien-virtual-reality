using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// taken and changed from https://github.com/SebLague/Kinematic-Equation-Problems
/// </summary>
public class BallLauncher : MonoBehaviour
{

    float timer;
    int waitingTime = 1;

    public GameObject snowballPrefab;
    public Transform targetPassed;

    public float height = 25;
    public float heightVariation = 10;
    public float gravity = -18;

    public float targetPosVariation = 4;

    public bool debugPath;

    GameObject[] players;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            //Action

            GameObject ball = Instantiate(snowballPrefab, transform.position, Quaternion.identity) as GameObject;
            GameObject targetPlayer = SelectPlayer();
            if(targetPlayer != null)
            {
                Launch(ball, targetPlayer.transform);
            }

            timer = 0;
        }

        if (debugPath)
        {
            DrawPath();
        }
    }

    private GameObject SelectPlayer()
    {
        if (players == null) return null;
        int randomIndex = Random.Range(0, players.Length - 1);
        GameObject targetPlayer = players[randomIndex];
        return targetPlayer;
    }


    void OnPlayerConnected(NetworkPlayer player)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Player " + " connected from " + player.ipAddress + ":" + player.port + "; No of players: " + players.Length);
    }

    void Launch(GameObject ball, Transform target)
    {

        Physics.gravity = Vector3.up * gravity;
        Rigidbody ballRigid = ball.GetComponent<Rigidbody>();
        ballRigid.useGravity = true;
        ballRigid.velocity = CalculateLaunchData(ballRigid, target).initialVelocity;

        NetworkServer.Spawn(ball);

        Destroy(ball, 10.0f);
    }

    LaunchData CalculateLaunchData(Rigidbody ball, Transform target)
    {
        //height variation
        float h = Random.Range(height- heightVariation, height+ heightVariation);

        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        //position variation 
        displacementXZ += new Vector3(Random.Range(-targetPosVariation, targetPosVariation), Random.Range(-targetPosVariation, targetPosVariation));

        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        //LaunchData launchData = CalculateLaunchData();
        //Vector3 previousDrawPoint = ball.position;

        //int resolution = 30;
        //for (int i = 1; i <= resolution; i++)
        //{
        //    float simulationTime = i / (float)resolution * launchData.timeToTarget;
        //    Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
        //    Vector3 drawPoint = ball.position + displacement;
        //    Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
        //    previousDrawPoint = drawPoint;
        //}
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

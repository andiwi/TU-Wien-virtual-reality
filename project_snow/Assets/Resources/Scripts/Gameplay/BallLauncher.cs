using UnityEngine;
using System.Collections;

/// <summary>
/// taken and changed from https://github.com/SebLague/Kinematic-Equation-Problems
/// </summary>
public class BallLauncher : MonoBehaviour
{

    float timer;
    int waitingTime = 1;

    public GameObject snowballPrefab;
    public Transform target;

    public float h = 25;
    public float gravity = -18;

    public bool debugPath;

    void Start()
    {
       
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            //Action

            GameObject ball = Instantiate(snowballPrefab);         
            ball.transform.position = transform.position;
            Launch(ball.GetComponent<Rigidbody>());
            timer = 0;
        }

        if (debugPath)
        {
            DrawPath();
        }
    }

    void Launch(Rigidbody ball)
    {

        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData(ball).initialVelocity;
    }

    LaunchData CalculateLaunchData(Rigidbody ball)
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
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

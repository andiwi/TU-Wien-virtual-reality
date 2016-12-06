using UnityEngine;
using System.Collections;

public class ViveSnowBallControl : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    private IEnumerator idlePulseEnumerator;
    private bool idlePulsing = false;


    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObj.index);
        idlePulseEnumerator = PulseVibration(10000, 0.15f, 0.5f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("You have collided with " + collider.name + " and activated OnTriggerStay");


        AuthorityManager colAuthMan = collider.gameObject.GetComponent<AuthorityManager>();

        if (colAuthMan != null)
        {

            StartIdlePulse();

            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("You have collided with " + collider.name + " while holding down Touch");

                colAuthMan.GrabObject(gameObject.transform);

                //collider.attachedRigidbody.isKinematic = true;  
                //collider.gameObject.transform.SetParent(gameObject.transform);
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("You have released Touch while colliding with " + collider.name);
                collider.gameObject.transform.SetParent(null);
                collider.attachedRigidbody.isKinematic = false;

                colAuthMan.UnGrabObjectButKeepAuthority();
                ThrowSnowball(collider.attachedRigidbody);
            }

        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("OnTriggerExit - stop pulsing");
        StopIdlePulse();
    }

    void ThrowSnowball(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);          
        }
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }

    }

    void StartIdlePulse()
    {
        StartCoroutine(idlePulseEnumerator);
        idlePulsing = true;
    }
    void StopIdlePulse()
    {
        StopCoroutine(idlePulseEnumerator);
        idlePulsing = false;
    }

    public void RumbleController(float duration, float strength)
    {
        StartCoroutine(PulseVibration(duration, strength));
    }



    IEnumerator PulseVibration(float duration, float strength)
    {
        strength = Mathf.Clamp01(strength);
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime <= duration)
        {
            int valveStrength = Mathf.RoundToInt(Mathf.Lerp(0, 3999, strength));

            device.TriggerHapticPulse((ushort)valveStrength);
            yield return null;
        }
    }


    /**
     * vibrationCount is how many vibrations
     * vibrationLength is how long each vibration should go for
     * gapLength is how long to wait between vibrations
     * strength is vibration strength from 0-1
     * */
    IEnumerator PulseVibration(int vibrationCount, float vibrationLength, float gapLength, float strength)
    {
        strength = Mathf.Clamp01(strength);
        for (int i = 0; i < vibrationCount; i++)
        {
            if (i != 0) yield return new WaitForSeconds(gapLength);
            yield return StartCoroutine(PulseVibration(vibrationLength, strength));
        }
    }
}

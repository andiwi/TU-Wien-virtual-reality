using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveControllerControl : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    bool carrying = false;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Pressed Trigger");

        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
        {

            GameObject sphere = GameObject.FindGameObjectWithTag("TestSphere");
            sphere.transform.position = new Vector3(-1, 1, -0.3f);
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
    }

    void OnTriggerStay(Collider collider)
    {

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("OnTriggerStay - Collided with collider: " + collider.name);
            if (carrying)
            {
                Debug.Log("dropping stick");
                collider.attachedRigidbody.isKinematic = false;
                Cue cue = collider.gameObject.GetComponent<Cue>();

                if (cue)
                {
                    //TODO set cue carrying null;
                    cue.DetachDevice(device);
                }
                else
                {
                    //device.transform.

                    collider.gameObject.transform.SetParent(null);
                }

                RumbleController(0.1f, 0.5f);
                carrying = false;
            }
            else
            {
                Debug.Log("Debug: devicePos is: " + device.transform.pos + " , thisTransform is: " + transform.position);
                collider.attachedRigidbody.isKinematic = true;
                Cue cue = collider.gameObject.GetComponent<Cue>();

                if (cue)
                {
                    cue.AttachDevice(device, transform);
                }
                else
                {
                    //move any object entering the controller collider (FOR DEBUGGING)
                    collider.gameObject.transform.SetParent(gameObject.transform);
                }

                RumbleController(0.2f, 1f);
                carrying = true;
            }
        }
    }

    void OnTriggerEnter()
    {
        if (!carrying)
        {
            idlePulseEnumerator = PulseVibration(10000, 0.5f, 0.5f, 0.5f);
            StartCoroutine(idlePulseEnumerator);
        }

    }

    void OnTriggerExit()
    {
        if (idlePulseEnumerator != null)
            StopCoroutine(idlePulseEnumerator);
    }


    void RumbleController(float duration, float strength)
    {
        StartCoroutine(PulseVibration(duration, strength));
    }

    private IEnumerator idlePulseEnumerator;

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


    //vibrationCount is how many vibrations
    //vibrationLength is how long each vibration should go for
    //gapLength is how long to wait between vibrations
    //strength is vibration strength from 0-1
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

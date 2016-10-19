using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveControllerInput : MonoBehaviour
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
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("OnTriggerEnter - Collided with collider: " + collider.name);
        if (!carrying && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("trigger Pushed");

            //TODO pulse when grabbing cueStick
            collider.attachedRigidbody.isKinematic = true;
            collider.gameObject.transform.SetParent(gameObject.transform);
            RumbleController(0.5f, 1f);
            carrying = true;
        }
        else if (carrying && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("dropping stick");
            collider.attachedRigidbody.isKinematic = false;
            collider.gameObject.transform.SetParent(null);
            carrying = false;
        }
        else
        {
            //TODO vibrating! (touched but not grabbed)
            LongVibration(2, 0.5f, 1f, 0.5f);
        }
    }

    private void removeCueStick()
    {
        //cue.transform.position = Vector3.zero;
        //TODO short pulse
    }



    void RumbleController(float duration, float strength)
    {
        StartCoroutine(RumbleControllerRoutine(duration, strength));
    }

    IEnumerator RumbleControllerRoutine(float duration, float strength)
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

    //not working yet TODO
    //vibrationCount is how many vibrations
    //vibrationLength is how long each vibration should go for
    //gapLength is how long to wait between vibrations
    //strength is vibration strength from 0-1
    IEnumerator LongVibration(int vibrationCount, float vibrationLength, float gapLength, float strength)
    {
        strength = Mathf.Clamp01(strength);
        for (int i = 0; i < vibrationCount; i++)
        {
            if (i != 0) yield return new WaitForSeconds(gapLength);
            yield return StartCoroutine(RumbleControllerRoutine(vibrationLength, strength));
        }
    }

}

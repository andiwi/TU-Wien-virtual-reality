using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveControllerControl : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    bool carrying = false;
    IViveControlControllable currentControllable;

    private IEnumerator idlePulseEnumerator;

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
            Debug.Log("OnTriggerStay - Touched down Trigger and collided with collider: " + collider.name);

            if (!carrying)
            {
                //Debug.Log("Debug: devicePos is: " + device.transform.pos + " , thisTransform is: " + transform.position);

                currentControllable = collider.gameObject.GetComponent<IViveControlControllable>();

                if (currentControllable != null)
                {
                    currentControllable.AttachDevice(device, transform);
                    //collider.attachedRigidbody.isKinematic = true;
                    RumbleController(0.2f, 1f);
                    carrying = true;
                    StartCoroutine(idlePulseEnumerator);
                }
            }
            else
            {
                dropControllable();
            }
        }
    }

    private void dropControllable()
    {
        Debug.Log("dropControllable() - dropping currentControllable: " + currentControllable);

        if (currentControllable != null)
        {
            currentControllable.DetachDevice(device);
            RumbleController(0.1f, 0.5f);
            carrying = false;
        }
    }


    void OnTriggerEnter()
    {
        if (!carrying)
        {
            idlePulseEnumerator = PulseVibration(10000, 0.2f, 0.5f, 0.1f);
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

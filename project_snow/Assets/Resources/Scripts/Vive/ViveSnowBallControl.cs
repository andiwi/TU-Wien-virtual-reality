using UnityEngine;
using System.Collections;

public class ViveSnowBallControl : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    private IEnumerator idlePulseEnumerator;
    private bool idlePulsing = false;


    // touch and trigger state - set by external components (TouchLeft/TouchRight)
    private bool touchingSnowBall = false;
    //private bool triggerDown = false;
    AuthorityManager touchedSnowballAuthMan;


    /// <summary>
    /// to be set by TouchLeft/TouchRight
    /// </summary>
    /// <param name="touching"></param>
    /// <param name="authManSnowBall"></param>
    public void SetTouchingSnowBall(bool touching, AuthorityManager authManSnowBall)
    {
        print("SetTouchingSnowBall touching: " + touching + " authMan: " + authManSnowBall);

        if (authManSnowBall == null && touchedSnowballAuthMan != null)
        {
            //when trigger exit and setting touchedSnowballAuthMan == null ->  Ungrab before deleting reference!
            touchedSnowballAuthMan.UnGrabObject();
        }
        touchingSnowBall = touching;
        touchedSnowballAuthMan = authManSnowBall;

    }

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObj.index);
        idlePulseEnumerator = PulseVibration(10000, 0.25f, 0.5f, 0.1f);
    }

    public void TryPickUpSnowBall()
    {
        if (touchingSnowBall && touchedSnowballAuthMan)
        {
            print("TryPickUpSnowBall + touchingSnowBall -> request Authority/Grabing");
            touchedSnowballAuthMan.GrabObject(gameObject.transform);
        }
        else
        {
            print("TryPickUpSnowBall - nice try - try touch snowball first...");
        }
    }

    public void TryLetGoSnowBall()
    {
        if (touchingSnowBall && touchedSnowballAuthMan)
        {
            print("TryLetGoSnowBall + touchingSnowBall -> ungrab object in authMan ");
            //touchedSnowballAuthMan.UnGrabObject();

            throwSnowball();
        }
        else
        {
            print("TryLetGoSnowBall - nice try - try touch snowball first...");
        }
    }


    void Update()
    {
        if (touchingSnowBall)
        {
            StartIdlePulse();
            //print("touching snow ball - start idle pulse if not started");
        }
        else
        {
            StopIdlePulse();
            //print("NOT touching snow ball - stop idle pulse if started");
        }
    }


    private void throwSnowball()
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            touchedSnowballAuthMan.ThrowObject(origin.TransformVector(device.velocity), origin.TransformVector(device.angularVelocity));
        }
        else
        {
            touchedSnowballAuthMan.ThrowObject(device.velocity, device.angularVelocity);
        }
    }

    void StartIdlePulse()
    {
        if (idlePulsing == false)
        {
            StartCoroutine(idlePulseEnumerator);
            idlePulsing = true;
        }
    }
    void StopIdlePulse()
    {
        if (idlePulsing == true)
        {
            StopCoroutine(idlePulseEnumerator);
            idlePulsing = false;
        }
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

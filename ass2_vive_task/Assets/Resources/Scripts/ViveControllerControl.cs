using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveControllerControl : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private bool carrying = false;
    private IViveControlControllable currentControllable;

    private IEnumerator idlePulseEnumerator;

    private Vector2 touchpad;

    private Vector3 newpos;
    private Vector3 oldpos;
    private Vector3 deltapos;

    private GameTransformator gameTransformator;

    private void calcDeltaPos()
    {
        newpos = transform.position;
        deltapos = (newpos - oldpos) / Time.deltaTime;
        oldpos = newpos;
        newpos = transform.position;
    }

    public float gameTransSmoothFactor = 5f;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    void Start()
    {
        GameObject game = GameObject.Find("Game");
        gameTransformator = game.GetComponent<GameTransformator>();
    }

    void Update()
    {
        calcDeltaPos();
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("Pushed Grip - going tanslate mode, curr deltapos: " + deltapos);       
            gameTransformator.translateGame(deltapos);
        }

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Read the touchpad values
            touchpad = device.GetAxis();

            Debug.Log("Touched Touchpad pos: " + touchpad);

            if (touchpad.y > 0.2f || touchpad.y < -0.2f)
            {
                
                //game.transform.localScale += (new Vector3(touchpad.y, touchpad.y, touchpad.y)) / gameTransSmoothFactor;
            }

        }
    

    }

    public OrientationHelper getOrientationHelber()
    {
        return transform.GetChild(1).gameObject.GetComponent<OrientationHelper>();
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
                    if (!currentControllable.isControllerAttached())
                    {
                        currentControllable.AttachController(this);

                        StopCoroutine(idlePulseEnumerator);
                        RumbleController(0.3f, 1f);

                    }
                    carrying = true;
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
            currentControllable.DetachController();

            RumbleController(0.1f, 0.5f);
            carrying = false;
        }
    }

    void OnJointBreak(float breakForce)
    {
        Debug.Log("OnJointBreak() called - joint apparently broke :( force: " + breakForce);
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

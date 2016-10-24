using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveControllerControl : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private IViveControlControllable currentControllable;

    private IEnumerator idlePulseEnumerator;
    private bool idlePulse = false;

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
        idlePulseEnumerator = PulseVibration(10000, 0.15f, 0.5f, 0.05f);
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

    /** 
     * gets the controller orientation helper */
    public OrientationHelper getOrientationHelber()
    {
        return transform.GetChild(1).gameObject.GetComponent<OrientationHelper>();
    }

    void StartIdlePulse()
    {
        StartCoroutine(idlePulseEnumerator);
        idlePulse = true;
    }
    void StopIdlePulse()
    {
        StopCoroutine(idlePulseEnumerator);
        idlePulse = false;
    }

    void OnTriggerStay(Collider collider)
    {
        currentControllable = collider.gameObject.GetComponent<IViveControlControllable>();

        if (currentControllable != null)
        {
            if (currentControllable.isControllerAttached())
            {
                StopIdlePulse();
            }
            else if (!idlePulse)
            {
                StartIdlePulse();
            }

            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("OnTriggerStay - Touched down Trigger and collided with collider: " + collider.name);

                //currentControllable = collider.gameObject.GetComponent<IViveControlControllable>();


                if (!currentControllable.isControllerAttached())
                {
                    //Debug.Log("Debug: devicePos is: " + device.transform.pos + " , thisTransform is: " + transform.position);

                    Debug.Log("attaching controller via " + gameObject.name);
                    currentControllable.AttachController(this);

                    RumbleController(0.4f, 1f);

                }
                else
                {
                    dropControllable();
                }
            }
        }
        else
        {
            StopIdlePulse();
        }


    }

    private void dropControllable()
    {
        Debug.Log("dropControllable() - dropping currentControllable: " + currentControllable);

        if (currentControllable != null)
        {
            currentControllable.DetachController();

            RumbleController(0.3f, 0.8f);
        }
    }

    void OnJointBreak(float breakForce)
    {
        Debug.Log("OnJointBreak() called - joint apparently broke :( force: " + breakForce);
    }


    //void OnTriggerEnter(Collider collider)
    //{
    //    IViveControlControllable controllable = collider.gameObject.GetComponent<IViveControlControllable>();

    //    if (controllable != null && !controllable.isControllerAttached())
    //    {
    //        idlePulseEnumerator = PulseVibration(10000, 0.2f, 0.5f, 0.1f);
    //        StartCoroutine(idlePulseEnumerator);
    //    }

    //}

    void OnTriggerExit(Collider collider)
    {
        IViveControlControllable controllable = collider.gameObject.GetComponent<IViveControlControllable>();

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && controllable != null && controllable.isControllerAttached())
        {
            dropControllable();
        }


        StopIdlePulse();
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

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

    Vector2 touchpad;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    private Vector3 newpos;
    private Vector3 oldpos;
    private Vector3 deltapos;

    private void calcDeltaPos()
    {
        newpos = transform.position;
        deltapos = (newpos - oldpos) / Time.deltaTime;
        oldpos = newpos;
        newpos = transform.position;
    }

    public float smoothFactor = 5f;

    void Update()
    {
        calcDeltaPos();
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("Pushed Grip - going tanslate mode, curr deltapos: " + deltapos);

            //GameObject balls = GameObject.Find("Balls");
            GameObject game = GameObject.Find("Game");

            //gamingBox.transform.position += deltapos;
            game.transform.position += (deltapos / smoothFactor);

            // game.transform.SetParent(transform);

        }

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Read the touchpad values
            touchpad = device.GetAxis();

            Debug.Log("Touched Touchpad pos: " + touchpad);

            if (touchpad.y > 0.2f || touchpad.y < -0.2f)
            {
                GameObject game = GameObject.Find("Game");
                game.transform.localScale += (new Vector3(touchpad.y, touchpad.y, touchpad.y))/ smoothFactor;
            }

        }

    }


    public void createFixedJoint(Rigidbody connectedBody)
    {
        GameObject child = transform.GetChild(1).gameObject;

        Rigidbody rigid = child.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
        rigid.useGravity = false;

        Orientation orient = child.GetComponent<Orientation>();

        //set initial rotation
        connectedBody.transform.rotation = orient.getRotation();
        Debug.Log("orientation: " + orient.getRotation());

        FixedJoint joint = child.AddComponent<FixedJoint>();
        joint.connectedBody = connectedBody;
        //joint.anchor = transform.position;
        Debug.Log("setFixedJoint for " + connectedBody.name);
    }

    public void destroyFixedJoint(Rigidbody connectedBody)
    {
        GameObject child = transform.GetChild(1).gameObject;
        Destroy(child.GetComponent<FixedJoint>());
        Destroy(child.GetComponent<Rigidbody>());
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
                        //collider.attachedRigidbody.isKinematic = true;
                        StopCoroutine(idlePulseEnumerator);
                        RumbleController(0.2f, 1f);

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
            currentControllable.DetachController(this);

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

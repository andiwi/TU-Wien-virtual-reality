using UnityEngine;
using System.Collections;
using Leap.Unity;

public class RaycastSelect : MonoBehaviour
{


    public bool selecting = false;
    public bool carrying = false;

    GameObject selectedObject;
    Vector3 fingerDir;
    Vector3 fingerPos;

    public LineRenderer lineRenderer;
    Ray ray;

    void Start()
    {
        ray = new Ray();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Alpha Blended"));
            lineRenderer.SetColors(Color.red, Color.red);
            lineRenderer.SetWidth(0.01F, 0.01F);
        }
    }


    void Update()
    {
        if (carrying)
        {
            carry(selectedObject);
        }
        else if (selecting)
        {
            CollisionCheck();
        }
    }

    private void CollisionCheck()
    {
        //Ray ray = Camera.current.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        // fingerDirection = hand.Fingers[selectedFinger].Bone(Bone.BoneType.TYPE_DISTAL).Direction.ToVector3();
        Leap.Finger finger = gameObject.GetComponent<CapsuleHand>().GetLeapHand().Fingers[1]; //TODO check if correct finger (INDEX)

        fingerDir = finger.Bone(Leap.Bone.BoneType.TYPE_DISTAL).Direction.ToVector3();
        fingerPos = finger.TipPosition.ToVector3();
        ray.origin = fingerPos;
        ray.direction = fingerDir;

        //   _anchor.transform.parent = transform.parent; //transform parent 


        RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit,Camera.main.farClipPlane, LayerMask.NameToLayer("Moveable")))
        if (Physics.Raycast(ray, out hit))
        {
            Moveable pickupObject = hit.collider.GetComponent<Moveable>();


            if (pickupObject != null)
            {


                if (selectedObject != pickupObject.gameObject)
                {
                    if (selectedObject != null)
                    {
                        //reset color of old selected object
                        selectedObject.GetComponent<Renderer>().material.color = Color.white;
                    }

                    //new selectedObject
                    selectedObject = pickupObject.gameObject;
                    selectedObject.GetComponent<Renderer>().material.color = Color.blue;

                }

                Debug.DrawRay(transform.position, transform.position + ray.direction * 10000, Color.blue);
                drawLine(transform.position, transform.position + ray.direction * 10000, Color.blue);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.position + ray.direction * 10000, Color.red);
                drawLine(transform.position, transform.position + ray.direction * 10000, Color.red);
            }

        }
        else
        {
            if (selectedObject != null)
            {
                //reset color of old selected object
                selectedObject.GetComponent<Renderer>().material.color = Color.white;
            }

            Debug.DrawRay(transform.position, transform.position + ray.direction * 10000, Color.red);
            drawLine(transform.position, transform.position + ray.direction * 10000, Color.red);

            selectedObject = null;
        }
    }

    public void StartSelecting()
    {
        selecting = true;
    }

    public void StopSelecting()
    {
        selecting = false;
    }

    public void StartCarrying()
    {
        Debug.Log("StartCarrying()");
        if (selectedObject != null)
        {
            Debug.Log("StartCarrying selectedObject ");
            carrying = true;
            selectedObject.GetComponent<Rigidbody>().isKinematic = true;
            selectedObject.GetComponent<Renderer>().material.color = Color.yellow;

        }

    }

    public void StopCarrying()
    {
        Debug.Log("StopCarrying()");
        if (selectedObject != null)
        {
            Debug.Log("StopCarrying selectedObject ");
            carrying = false;
            selectedObject.GetComponent<Rigidbody>().isKinematic = false;
            selectedObject.gameObject.GetComponent<Renderer>().material.color = Color.white;
            //selectedObject.transform.parent = null;
        }
    }

    private void drawLine(Vector3 start, Vector3 end, Color color)
    {
        Vector3 offset = new Vector3(0, 0, 1);
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end + offset);
        lineRenderer.SetColors(color, color);
    }

    private void carry(GameObject foo)
    {

        foo.transform.position = Camera.main.transform.position + Camera.main.transform.forward * Vector3.Distance(Camera.main.transform.position, foo.transform.position);

        //foo.transform.parent = transform; //TODO maybe change back
        Debug.DrawRay(transform.position, foo.transform.position, Color.yellow);
        drawLine(transform.position, foo.transform.position, Color.yellow);
    }
}

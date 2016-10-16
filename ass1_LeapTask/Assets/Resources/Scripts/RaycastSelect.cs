using UnityEngine;
using System.Collections;
using Leap.Unity;

/** 
 * Script providing object selection and carrying via raycast collision check
 * - has to be added to the gameObject containting a capsuleHand
 */
public class RaycastSelect : MonoBehaviour
{

    [Tooltip("Specify a new line renderer - optional")]
    public LineRenderer lineRenderer = null;

    private bool selecting = false;
    private bool carrying = false;
    private bool palmShooting = false;

    private GameObject selectedObject;
    private Vector3 shootingDir;
    private Vector3 shootingStartPos;
    private Ray ray;
    private CapsuleHand capsuleHand;

    void Start()
    {
        ray = new Ray();
        capsuleHand = gameObject.GetComponent<CapsuleHand>();

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
        determineShootingPositioning();

        if (carrying)
        {
            carry(selectedObject);
        }
        else if (selecting)
        {
            collisionCheck();
        }
        else
        {
            resetSelectedObjectColor();
            lineRenderer.enabled = false;
            selectedObject = null;
        }
    }

    private void determineShootingPositioning()
    {
        if (palmShooting)
        {
            shootingDir = capsuleHand.GetLeapHand().PalmNormal.Normalized.ToVector3();
            shootingStartPos = capsuleHand.GetLeapHand().PalmPosition.ToVector3();
        }
        else
        {
            Leap.Finger finger = capsuleHand.GetLeapHand().Fingers[1];
            shootingDir = finger.Bone(Leap.Bone.BoneType.TYPE_DISTAL).Direction.ToVector3();
            shootingStartPos = finger.TipPosition.ToVector3();
        }
    }

    private void collisionCheck()
    {
        ray.origin = shootingStartPos;
        ray.direction = shootingDir;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Moveable pickupObject = hit.collider.GetComponent<Moveable>();

            if (pickupObject != null)
            {
                if (selectedObject != pickupObject.gameObject)
                {
                    resetSelectedObjectColor();
                    //new selectedObject
                    selectedObject = pickupObject.gameObject;
                }

                selectedObject.GetComponent<Renderer>().material.color = Color.blue;
                drawLine(shootingStartPos, shootingStartPos + ray.direction * 10000, Color.blue);
            }
            else
            {
                resetSelectedObjectColor();             
                drawLine(shootingStartPos, shootingStartPos + ray.direction * 10000, Color.red);
            }

        }
        else
        {
            resetSelectedObjectColor();
            drawLine(shootingStartPos, shootingStartPos + ray.direction * 10000, Color.red);

            selectedObject = null;
        }
    }

    /**
     *  resets color of old selected object (if not null)
     */
    private void resetSelectedObjectColor()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material.color = Color.white;
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
        if (selectedObject != null)
        {
            Debug.Log("StopCarrying selectedObject ");
            carrying = false;
            selectedObject.GetComponent<Rigidbody>().isKinematic = false;
            selectedObject.gameObject.GetComponent<Renderer>().material.color = Color.white;
            selectedObject.transform.parent = null;
        }
    }

    public void StartPalmShooting()
    {
        //Debug.Log("StartPalmShooting");
        palmShooting = true;
    }

    public void StopPalmShooting()
    {
        //Debug.Log("StopPalmShooting");
        palmShooting = false;
    }


    private void drawLine(Vector3 start, Vector3 end, Color color)
    {
        Debug.DrawRay(start, end, color);
        Vector3 offset = new Vector3(0, 0, 1);
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end + offset);
        lineRenderer.SetColors(color, color);
        lineRenderer.enabled = true;
    }

    private void carry(GameObject foo)
    {
        foo.transform.parent = transform;
        drawLine(shootingStartPos, foo.transform.position, Color.yellow);
    }

    private void OnDestroy()
    {
        Destroy(lineRenderer.material);
    }
}

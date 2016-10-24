using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTransformator : MonoBehaviour
{


    public float smoothFactor = 10f;

    ViveControllerControl currControl1;
    ViveControllerControl currControl2;

    void Start()
    {

    }

    void Update()
    {
        //transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        rotateGame();
    }

    public void resetGame()
    {
        SceneManager.LoadScene("scene_main");

    }

    public void AttachRotator(ViveControllerControl control)
    {
        if (currControl1 == null && "Controller (left)".Equals(control.gameObject.name))
        {
            currControl1 = control;
        }
        else if (currControl2 == null && "Controller (right)".Equals(control.gameObject.name))
        {
            currControl2 = control;
        }
    }

    public void DetachRotator(ViveControllerControl control)
    {
        if ("Controller (left)".Equals(control.gameObject.name))
        {
            currControl1 = null;
        }
        else if ("Controller (right)".Equals(control.gameObject.name))
        {
            currControl2 = null;
        }
        else
        {
            currControl1 = null;
            currControl2 = null;
        }
    }

    private void rotateGame()
    {
        //check if currently both controls present (=pressing grip)
        if (currControl1 != null && currControl2 != null)
        {
            //currControl2.transform - currControl1

            Vector3 dir = currControl2.transform.position - currControl1.transform.position;
            Debug.Log("rotating game - " + dir);
            transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        }
    }

    public void translateGame(Vector3 deltaPos)
    {
        transform.position += (deltaPos / smoothFactor);
    }

    public void scaleGame(Vector3 scale)
    {

        if ((transform.localScale.x <= 0.2 && scale.x >= 0) ||
            (transform.localScale.x >= 0.2 && transform.localScale.x <= 10) ||
            (transform.localScale.x >= 10 && scale.x <= 0))
        {
            transform.localScale += scale / (smoothFactor * 10);
        }
    }

}

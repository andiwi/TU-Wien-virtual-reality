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
    }

    public void resetGame()
    {
        SceneManager.LoadScene("scene_main");

    }

    public void rotateGame(ViveControllerControl control)
    {
        //TODO
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
            transform.localScale += scale / (smoothFactor*10);
        }
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTransformator : MonoBehaviour {


    public float smoothFactor = 10f;

    ViveControllerControl currControl1;
    ViveControllerControl currControl2;

    void Start () {

        
	}
	
	void Update () {
        //transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }

    public void resetGame()
    {
        SceneManager.LoadScene("scene_main");
        
    }

    public void rotateGame(ViveControllerControl control)
    {

    }

    public void translateGame(Vector3 deltaPos)
    {
        transform.position += (deltaPos / smoothFactor);
    }

    public void scaleGame(Vector3 scale)
    {
        transform.localScale += scale / smoothFactor;
    }

    //private void calcDeltaPos()
    //{
    //    newpos = transform.position;
    //    deltapos = (newpos - oldpos) / Time.deltaTime;
    //    oldpos = newpos;
    //    newpos = transform.position;
    //}

}

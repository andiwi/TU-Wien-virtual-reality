using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// event manager 
/// </summary>
public class EventManager : NetworkBehaviour {


    public delegate void ClickAction();
    public static event ClickAction OnAuthorityAssigned;



    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
    //    {
    //        if (OnClicked != null)
    //            OnClicked();
    //    }
    //}
}

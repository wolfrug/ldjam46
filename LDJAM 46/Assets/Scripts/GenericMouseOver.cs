using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GenericMouseOverEvent : UnityEvent<GenericMouseOver> { }

[System.Serializable]
public class GenericMouseExitEvent : UnityEvent<GenericMouseOver> { }

[System.Serializable]
public class GenericMouseLeftClickEvent : UnityEvent<GenericMouseOver> { }

[System.Serializable]
public class GenericMouseRightClickEvent : UnityEvent<GenericMouseOver> { }

[System.Serializable]
public class GenericMouseMiddleClickEvent : UnityEvent<GenericMouseOver> { }

[RequireComponent (typeof (Collider))]
public class GenericMouseOver : MonoBehaviour {

    public string id = "generic";
    public GenericMouseOverEvent mouseOverEvent;
    public GenericMouseExitEvent mouseExitEvent;
    public GenericMouseLeftClickEvent mouseLeftClickEvent;
    public GenericMouseRightClickEvent mouseRightClickEvent;
    public GenericMouseMiddleClickEvent mouseMiddleClickEvent;

    public bool mouseIsOver = false;
    void OnMouseOver () {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log ("Mouse is over GameObject.");
        mouseOverEvent.Invoke (this);
        mouseIsOver = true;
    }

    void OnMouseExit () {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log ("Mouse is no longer on GameObject.");
        mouseExitEvent.Invoke (this);
        mouseIsOver = false;
    }

    void Update () {
        if (mouseIsOver) {
            if (Input.GetMouseButtonDown (0))
                //Debug.Log ("Pressed primary button.");
                mouseLeftClickEvent.Invoke (this);

            if (Input.GetMouseButtonDown (1))
                //Debug.Log ("Pressed secondary button.");
                mouseRightClickEvent.Invoke (this);

            if (Input.GetMouseButtonDown (2))
                //Debug.Log ("Pressed middle click.");
                mouseMiddleClickEvent.Invoke (this);
        }
    }
}
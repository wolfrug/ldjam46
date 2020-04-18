using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GenericMouseOver[] allSelectables;
    public Material mouseOverSelectedMat;
    public Material mouseOverDeselectedMat;
    public bool canSelectNext = true;
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (this);
        }
    }
    // Start is called before the first frame update
    void Start () {
        InkWriter.main.StartStory ();
        InkWriter.main.story.ObserveVariable ("canSelectNext", (string varName, object newValue) => {
            GameManager.instance.EventListenerCanSelect (varName, (int) newValue);
        });
        SelectableSetup ();
    }

    public void EventListenerCanSelect (string tag, int valuechange) {
        Debug.Log ("Var changed: " + tag + valuechange);
        canSelectNext = valuechange > 0;
        SelectableDeselectAll ();
    }

    void SelectableSetup () {
        allSelectables = FindObjectsOfType<GenericMouseOver> ();
        foreach (GenericMouseOver selectable in allSelectables) {
            selectable.mouseOverEvent.AddListener (SelectableSetMaterialSelect);
            selectable.mouseExitEvent.AddListener (SelectableSetMaterialDeSelect);
            selectable.mouseLeftClickEvent.AddListener (SelectableClick);
        }
    }
    void SelectableSetMaterialSelect (GenericMouseOver obj) {
        //Debug.Log ("Mouse over: " + obj.id, obj.gameObject);
        if (canSelectNext) {
            Renderer rend = obj.gameObject.GetComponent<MeshRenderer> ();
            rend.material = mouseOverSelectedMat;
        };
    }
    void SelectableSetMaterialDeSelect (GenericMouseOver obj) {
        if (canSelectNext) {
            Renderer rend = obj.gameObject.GetComponent<MeshRenderer> ();
            rend.material = mouseOverDeselectedMat;
        };
    }
    void SelectableDeselectAll () {
        foreach (GenericMouseOver obj in allSelectables) {
            SelectableSetMaterialDeSelect (obj);
        }
    }
    void SelectableClick (GenericMouseOver obj) {
        Debug.Log ("Mouse clicked: " + obj.id, obj.gameObject);
        if (canSelectNext) {
            //canSelectNext = false;
            InkWriter.main.story.variablesState[("canSelectNext")] = 0;
            InkWriter.main.GoToKnot ("start_" + obj.id);
        }
    }

    public void SaveGame(){
        InkWriter.main.SaveStory();
    }

    // Update is called once per frame
    void Update () {

    }
}
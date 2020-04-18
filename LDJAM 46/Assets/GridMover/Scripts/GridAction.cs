using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GridObject))]
public class GridAction : MonoBehaviour {
    private GridObject self;
    public List<GridObjectInteractDataBase> allActions = new List<GridObjectInteractDataBase> { };
    private Dictionary<string, GridObjectInteractDataBase> allActionIDDictionary = new Dictionary<string, GridObjectInteractDataBase> { };
    void Awake () {
        if (self == null) {
            self = GetComponent<GridObject> ();
        }
    }

    public void SetActions (GridObjectInteractDataBase[] newActions) {
        allActions.Clear ();
        foreach (GridObjectInteractDataBase action in newActions) {
            allActions.Add (action);
            allActionIDDictionary.Add (action.id, action);
        }
    }

    public void DoAction (string id) {
        GridObjectInteractDataBase outValue;
        allActionIDDictionary.TryGetValue (id, out outValue);
        if (outValue != null) {
            outValue.DoAction (self);
        }
    }
    public void DoAction (GridObjectInteractDataBase action) {
        if (allActions.Contains (action)) {
            action.DoAction (self);
        }
    }
}
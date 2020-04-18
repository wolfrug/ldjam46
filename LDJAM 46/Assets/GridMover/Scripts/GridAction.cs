using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GridObject))]
public class GridAction : MonoBehaviour {
    private GridObject self;
    public List<GridObjectInteractDataBase> allActionsComplete = new List<GridObjectInteractDataBase> { };
    public List<GridObjectInteractDataBase> activeActions = new List<GridObjectInteractDataBase> { };
    private Dictionary<string, GridObjectInteractDataBase> allActionIDDictionary = new Dictionary<string, GridObjectInteractDataBase> { };
    void Awake () {
        if (self == null) {
            self = GetComponent<GridObject> ();
        }
    }

    public void SetActions (GridObjectInteractDataBase[] newActions, bool setActiveActions = true) {
        allActionsComplete.Clear ();
        foreach (GridObjectInteractDataBase action in newActions) {
            allActionsComplete.Add (action);
            allActionIDDictionary.Add (action.id, action);
        }
        if (setActiveActions) { // hard coded!!
            SetActiveActions (3, true);
        };
    }

    public void SetActiveActions (int amount, bool uniqueOnly) {
        activeActions.Clear ();
        activeActions = GetRandomActions (amount, uniqueOnly);
    }

    public void DoAction (string id) {
        GridObjectInteractDataBase outValue;
        allActionIDDictionary.TryGetValue (id, out outValue);
        if (outValue != null) {
            outValue.DoAction (self);
        }
    }
    public void DoAction (GridObjectInteractDataBase action) {
        if (allActionsComplete.Contains (action)) {
            action.DoAction (self);
        }
    }
    public void AddAction (GridObjectInteractDataBase action) {
        allActionsComplete.Add (action);
        if (!allActionIDDictionary.ContainsKey (action.id)) {
            allActionIDDictionary.Add (action.id, action);
        }
    }
    public void RemoveAction (GridObjectInteractDataBase action) {
        if (allActionsComplete.Contains (action)) {
            allActionsComplete.Remove (action);
        }
        if (allActionIDDictionary.ContainsKey (action.id)) {
            allActionIDDictionary.Remove (action.id);
        }
    }
    public List<GridObjectInteractDataBase> GetRandomActions (int amount, bool uniqueOnly = true) {
        List<GridObjectInteractDataBase> returnList = new List<GridObjectInteractDataBase> ();
        List<GridObjectInteractDataBase> copyOfAllActionsList = new List<GridObjectInteractDataBase> ();
        foreach (GridObjectInteractDataBase act in allActionsComplete) {
            copyOfAllActionsList.Add (act);
        }
        if (copyOfAllActionsList.Count > 0) {
            for (int i = 0; i < amount; i++) {
                GridObjectInteractDataBase randomAction = copyOfAllActionsList[Random.Range (0, copyOfAllActionsList.Count)];
                returnList.Add (randomAction);
                copyOfAllActionsList.Remove (randomAction);
                if (uniqueOnly) { // if we only add one of each, remove all other examples at this point
                    copyOfAllActionsList.RemoveAll (s => s.type == randomAction.type);
                }
                if (copyOfAllActionsList.Count == 0) {
                    break;
                }
            }
        };
        return returnList;
    }

    public GridObjectInteractDataBase GetAction (string id) {
        GridObjectInteractDataBase returnAction = null;
        allActionIDDictionary.TryGetValue (id, out returnAction);
        return returnAction;
    }
}
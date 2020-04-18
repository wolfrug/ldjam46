using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "GridObjectInteractionData", order = 1)]
public class GridObjectInteractDataBase : ScriptableObject {
    public string id;

    public virtual void DoAction (GridObject self) {
        Debug.Log ("Do the action!");
    }
    public List<GridObject> GetGridObject (GridObject self, int distance, GridObjectFacing facing = GridObjectFacing.NONE) {
        // Tries to get gridobjects within X squares / facing of self
        List<GridObject> returnGOS = new List<GridObject> { };
        List<GridObject> allGridObjects = GridObjectManager.instance.allGridObjects;
        if (facing == GridObjectFacing.NONE) { // no facing, so just get all within the distance given
            for (int i = 0; i < allGridObjects.Count; i++) {
                if (allGridObjects[i] != self) {
                    if (GridObjectManager.instance.GridObjectDistance (self, allGridObjects[i]) <= distance) {
                        returnGOS.Add (allGridObjects[i]);
                    }
                }
            }
            return returnGOS;
        } else { // otherwise we take self-facing, then check every grid object in the right X or Y line

            Vector3Int selfLocation = self.pathfinder.selfLocation;
            Vector3Int otherLocation = Vector3Int.zero;
            foreach (GridObject obj in allGridObjects) {
                int straightDistance = GridObjectManager.instance.GridObjectDistanceLine (self, obj);
                if (straightDistance > 0 && straightDistance <= distance) { // i.e. they're on a line in SOME direction, and close enough
                    if (GridObjectManager.instance.IsFacing (self, obj, facing)) {
                        returnGOS.Add (obj);
                    }
                };
            }
        }
        return returnGOS;
    }
    public List<GridObject> GetGridObject (GridObject self, int distance, Vector2Int facing) {
        return GetGridObject (self, distance, GridObjectManager.instance.ConvertNumbersToFacingEum (facing));
    }

}
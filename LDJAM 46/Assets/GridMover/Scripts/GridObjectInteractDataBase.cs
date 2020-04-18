using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridActionFacing {
    NONE = 9999,
    FORWARD = 0000,
    RIGHT = 1000,
    LEFT = 2000,
    BACK = 3000,
}

public enum GridActionType {
    NONE = 0000,
    ATTACK = 1000,
    MOVE = 2000,
    SPECIAL = 3000
}

[CreateAssetMenu (fileName = "Data", menuName = "GridObjectInteractionData", order = 1)]
public class GridObjectInteractDataBase : ScriptableObject {
    public string id;
    public string actionName;
    public GridActionType type = GridActionType.NONE;
    public int actionCost = 1;
    [Multiline]
    public string actionDescription;
    public Sprite actionIcon;
    public GameObject cardPrefab;

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

    public GridObjectFacing ConvertRelativeFacingToAbsolute (GridActionFacing target, GridObjectFacing direction) {
        // Converts a direction facing to relative facing, i.e. if you look up-left then up-right is facing-right
        GridObjectFacing result = GridObjectFacing.NONE;
        switch (direction) {
            case GridObjectFacing.UP_LEFT:
                {
                    switch (target) {
                        case GridActionFacing.RIGHT:
                            {
                                result = GridObjectFacing.UP_RIGHT;
                                break;
                            }
                        case GridActionFacing.LEFT:
                            {
                                result = GridObjectFacing.DOWN_LEFT;
                                break;
                            }
                        case GridActionFacing.FORWARD:
                            {
                                result = direction;
                                break;
                            }
                        case GridActionFacing.BACK:
                            {
                                result = GridObjectFacing.DOWN_RIGHT;
                                break;
                            }
                    }
                    break;
                }
            case GridObjectFacing.UP_RIGHT:
                {
                    switch (target) {
                        case GridActionFacing.RIGHT:
                            {
                                result = GridObjectFacing.DOWN_RIGHT;
                                break;
                            }
                        case GridActionFacing.LEFT:
                            {
                                result = GridObjectFacing.UP_LEFT;
                                break;
                            }
                        case GridActionFacing.FORWARD:
                            {
                                result = direction;
                                break;
                            }
                        case GridActionFacing.BACK:
                            {
                                result = GridObjectFacing.DOWN_LEFT;
                                break;
                            }
                    }
                    break;
                }
            case GridObjectFacing.DOWN_LEFT:
                {
                    switch (target) {
                        case GridActionFacing.RIGHT:
                            {
                                result = GridObjectFacing.UP_LEFT;
                                break;
                            }
                        case GridActionFacing.LEFT:
                            {
                                result = GridObjectFacing.DOWN_RIGHT;
                                break;
                            }
                        case GridActionFacing.FORWARD:
                            {
                                result = direction;
                                break;
                            }
                        case GridActionFacing.BACK:
                            {
                                result = GridObjectFacing.UP_RIGHT;
                                break;
                            }
                    }
                    break;
                }
            case GridObjectFacing.DOWN_RIGHT:
                {
                    switch (target) {
                        case GridActionFacing.RIGHT:
                            {
                                result = GridObjectFacing.DOWN_LEFT;
                                break;
                            }
                        case GridActionFacing.LEFT:
                            {
                                result = GridObjectFacing.UP_RIGHT;
                                break;
                            }
                        case GridActionFacing.FORWARD:
                            {
                                result = direction;
                                break;
                            }
                        case GridActionFacing.BACK:
                            {
                                result = GridObjectFacing.UP_LEFT;
                                break;
                            }
                    }
                    break;
                }
        }
        return result;
    }
    public GridObjectFacing ConvertRelativeFacingToAbsolute (GridActionFacing target, Vector2Int direction) {
        return ConvertRelativeFacingToAbsolute (target, GridObjectManager.instance.ConvertNumbersToFacingEum (direction));
    }

}
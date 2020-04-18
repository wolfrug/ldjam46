using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridObjectManager : MonoBehaviour {
    public static GridObjectManager instance;
    private List<GridObject> allGridObjectsMain = new List<GridObject> { };

    private List<GridObject> allPlayableObjectsMain = new List<GridObject> { };
    private List<GridObject> allEnemyObjectsMain = new List<GridObject> { };
    public Grid mainGrid;
    public GridObjectDie objectDieEvent;
    // Start is called before the first frame update

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (this);
        }
    }
    void Start () {
        if (mainGrid == null) {
            mainGrid = GridManager.instance.maingrid;
        }
    }

    void GridObjectDie (GridObject obj) {
        Debug.Log ("Death: " + obj, obj.gameObject);
        objectDieEvent.Invoke (obj);
    }

    public void UpdateGridObjectListSlow () {
        allGridObjectsMain.Clear ();
        allPlayableObjectsMain.Clear ();
        allEnemyObjectsMain.Clear ();
        foreach (GridObject obj in FindObjectsOfType<GridObject> ()) {
            allGridObjectsMain.Add (obj);
            obj.deathEvent.RemoveListener (GridObjectDie);
            obj.deathEvent.AddListener (GridObjectDie);
            if (obj.data.type_ == GridObjectType.PLAYABLE) {
                allPlayableObjectsMain.Add (obj);
            }
            if (obj.data.type_ == GridObjectType.ENEMY) {
                allEnemyObjectsMain.Add (obj);
            }
        }
    }

    public void RefreshAllObjects () {
        foreach (GridObject obj in allGridObjectsMain) {
            // refresh action points
            obj.actionpoints = obj.data.actionpoints;
            // create new lists of actions ALSO HARD CODED
            obj.action.SetActiveActions (3, true);
        }
    }

    public List<GridObject> allGridObjects {
        get {
            if (allGridObjectsMain.Count == 0) {
                UpdateGridObjectListSlow ();
            }
            return allGridObjectsMain;
        }
    }
    public List<GridObject> allPlayableObjects {
        get {
            if (allPlayableObjectsMain.Count == 0) {
                UpdateGridObjectListSlow ();
            }
            return allPlayableObjectsMain;
        }
    }
    public List<GridObject> allEnemyObjects {
        get {
            if (allEnemyObjectsMain.Count == 0) {
                UpdateGridObjectListSlow ();
            }
            return allEnemyObjectsMain;
        }
    }

    public int LivingEntities (GridObjectType type) {
        int living = 0;
        foreach (GridObject obj in allGridObjects) {
            if (!obj.dead && obj.data.type_ == type) {
                living++;
            }
        }
        return living;
    }

    public int GridObjectDistance (GridObject one, GridObject two) { // calculate the number of squares between two objects. very fast
        List<Spot> path = GridManager.instance.MakePath (one.pathfinder.originPoint.position, two.pathfinder.originPoint.position, 1000, false);
        int steps = -1;
        if (path != null) {
            steps = path.Count - 1;
        }
        return steps;
    }
    public int GridObjectDistanceLine (GridObject one, GridObject two) { // distance, if possible, in a straight line between two objects
        Vector3Int spot1 = one.pathfinder.selfLocation;
        Vector3Int spot2 = two.pathfinder.selfLocation;
        if (spot1.x != spot2.x && spot1.y != spot2.y) { // not in line!
            return -1;
        }
        if (spot1.x == spot2.x) {
            return Mathf.Abs (spot1.y - spot2.y);
        } else {
            return Mathf.Abs (spot1.x - spot2.x);
        }
    }
    public bool IsFacing (GridObject facer, GridObject facee, GridObjectFacing optionalFacing = GridObjectFacing.NONE) { // returns whether facer is facing facee
        Vector2Int facing = new Vector2Int (0, 0);
        if (optionalFacing == GridObjectFacing.NONE) { // default -> we grab the facing from the facer
            facing = facer.facing;
        } else {
            facing = ConvertFacingEnumToNumbers (optionalFacing);
        }
        if (GridObjectDistanceLine (facer, facee) > 0) { // are they in a line?
            if (facing.x == 1) { // facing up right
                return (facer.pathfinder.selfLocation.x < facee.pathfinder.selfLocation.x);
            } else if (facing.x == -1) { // facing down left
                return (facer.pathfinder.selfLocation.x > facee.pathfinder.selfLocation.x);
            } else if (facing.y == 1) { // facing up left
                return (facer.pathfinder.selfLocation.y < facee.pathfinder.selfLocation.y);
            } else if (facing.y == -1) { // facing down right
                return (facer.pathfinder.selfLocation.y > facee.pathfinder.selfLocation.y);
            }
        }
        return false;
    }

    public Vector2Int ConvertFacingEnumToNumbers (GridObjectFacing facing) {
        Vector2Int returnVal = new Vector2Int (-1, -1);
        switch (facing) {
            case GridObjectFacing.DOWN_LEFT:
                {
                    returnVal.x = -1;
                    returnVal.y = 0;
                    break;
                }
            case GridObjectFacing.DOWN_RIGHT:
                {
                    returnVal.x = 0;
                    returnVal.y = -1;
                    break;
                }
            case GridObjectFacing.UP_LEFT:
                {
                    returnVal.x = 0;
                    returnVal.y = 1;
                    break;
                }
            case GridObjectFacing.UP_RIGHT:
                {
                    returnVal.x = 1;
                    returnVal.y = 0;
                    break;
                }
        }
        return returnVal;
    }
    public GridObjectFacing ConvertNumbersToFacingEum (Vector2Int facing) {
        GridObjectFacing returnVal = GridObjectFacing.NONE;
        if (facing.x == -1 && facing.y == 0) {
            returnVal = GridObjectFacing.DOWN_LEFT;
        } else if (facing.x == 0 && facing.y == -1) {
            returnVal = GridObjectFacing.DOWN_RIGHT;
        } else if (facing.x == 0 && facing.y == 1) {
            returnVal = GridObjectFacing.UP_LEFT;
        } else if (facing.x == 1 && facing.y == 0) {
            returnVal = GridObjectFacing.UP_RIGHT;
        }
        return returnVal;
    }

    // Update is called once per frame
    void Update () {

    }
}
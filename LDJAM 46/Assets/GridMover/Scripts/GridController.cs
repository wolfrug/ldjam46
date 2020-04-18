using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridController : MonoBehaviour {
    private Grid gridObj;
    private Tilemap tilemapobj;
    void Awake () {
        if (gridObj == null) {
            gridObj = GetComponentInParent<Grid> ();
        }
    }

    public Grid grid {
        get {
            if (gridObj == null) {
                gridObj = GetComponentInParent<Grid> ();
            }
            return gridObj;
        }
        set { // Don't do this
            gridObj = value;
        }
    }

    public virtual Tilemap tilemap {
        get {
            if (tilemapobj == null) {
                tilemapobj = GetComponentInChildren<Tilemap> ();
            }
            return tilemapobj;
        }
    }

    public Vector3Int WorldSpaceToGrid (Vector3 worldspaceLoc) {
        Vector3Int returnVal = grid.WorldToCell (worldspaceLoc);
        return returnVal;
    }
    public TileBase GetTile (Vector3Int targetLoc) {
        return tilemap.GetTile (targetLoc);
    }
    public TileBase GetTile (Vector3 worldSpaceLoc) {
        return tilemap.GetTile (WorldSpaceToGrid (worldSpaceLoc));
    }
    public bool HasTile (Vector3Int targetLoc) {
        return tilemap.HasTile (targetLoc);
    }
    public bool HasTile (Vector3 worldspaceLoc) {
        return tilemap.HasTile (WorldSpaceToGrid (worldspaceLoc));
    }
}
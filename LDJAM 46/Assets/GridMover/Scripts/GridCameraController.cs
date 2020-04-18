using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public struct TileInfo {
    public TileBase tile;
    public Tilemap tilemap;
    public Vector3Int location;
}

[System.Serializable]
public class GridMouseClickEvent : UnityEvent<TileInfo> { }

[System.Serializable]
public class GridMouseOverEvent : GridMouseClickEvent { }

[System.Serializable]
public class GridMouseExitEvent : GridMouseClickEvent { }

[System.Serializable]
public class GridMouseLeftClickEvent : GridMouseClickEvent { }

[System.Serializable]
public class GridMouseRightClickEvent : GridMouseClickEvent { }

[System.Serializable]
public class GridMouseMiddleClickEvent : GridMouseClickEvent { }
public class GridCameraController : MonoBehaviour {

    public static GridCameraController instance;
    // Start is called before the first frame update
    public Camera mainCam;
    public float cameraDistance = 500f;
    private Transform cameraTarget;
    public CinemachineVirtualCamera mainvcam;
    public LayerMask hitMask;
    public GridMouseOverEvent mouseOverEvent;
    public GridMouseExitEvent mouseExitEvent;
    public GridMouseLeftClickEvent mouseLeftClickEvent;
    public GridMouseRightClickEvent mouseRightClickEvent;
    public GridMouseMiddleClickEvent mouseMiddleClickEvent;
    public TileInfo mouseOverTile;
    private TileInfo previousMouseOverTile;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (this);
        }

    }

    void Start () {
        if (mainCam == null) {
            mainCam = Camera.main;
        }
        previousMouseOverTile = TileInfoConstructor (null, null, Vector3Int.zero);
    }

    public void SetCamTarget (Transform newtarget) {
        mainvcam.m_LookAt = newtarget;
        mainvcam.m_Follow = newtarget;
    }

    TileInfo TileInfoConstructor (TileBase tile, Tilemap tilemap, Vector3Int location) {
        TileInfo newinfo;
        newinfo.tile = tile;
        newinfo.tilemap = tilemap;
        newinfo.location = location;
        return newinfo;
    }
    void NullPreviousTile () {
        previousMouseOverTile.tile = null;
        previousMouseOverTile.tilemap = null;
        previousMouseOverTile.location = Vector3Int.zero;
    }

    TileInfo DidHit (RaycastHit2D hit, GridMouseClickEvent clickEvent) {
        TileInfo info = TileInfoConstructor (null, null, Vector3Int.zero);
        if (hit.collider != null) {
            //Debug.Log (hit.collider);
            GridController controller = hit.transform.GetComponentInParent<GridController> () as GridController;
            if (controller != null) {
                TileBase tile = controller.GetTile (hit.point);
                if (tile != null) {
                    info.tile = tile;
                    info.tilemap = controller.tilemap;
                    info.location = controller.WorldSpaceToGrid (hit.point);
                    clickEvent.Invoke (info);
                    //Debug.Log ("Tile: " + tile.name + " Tilemap: " + controller.tilemap.name);
                    return info;
                }
            }
        }
        return info;
    }

    // Update is called once per frame
    void Update () {
        RaycastHit2D hit = Physics2D.Raycast (mainCam.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, cameraDistance, hitMask);
        if (Input.GetAxis ("Fire1") > 0f) {
            //Debug.Log ("Clicked");
            //Debug.DrawRay (mainCam.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Color.red, 3f);
            DidHit (hit, mouseLeftClickEvent);
        }
        if (Input.GetAxis ("Fire2") > 0f) { // we don't actually do anything with this but run the event lol
            //Debug.Log ("Clicked");
            //Debug.DrawRay (mainCam.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Color.red, 3f);
            mouseRightClickEvent.Invoke (TileInfoConstructor (null, null, new Vector3Int (0, 0, 0)));
        }

        // Mouse over
        //Debug.DrawRay (mainCam.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Color.red, 3f);
        mouseOverTile = DidHit (hit, mouseOverEvent);
        if (previousMouseOverTile.tile == null && mouseOverTile.tile != null) {
            //Debug.Log ("PreviousHighlightTile: " + mouseOverTile.tile);
            previousMouseOverTile.tile = mouseOverTile.tile;
            previousMouseOverTile.tilemap = mouseOverTile.tilemap;
            previousMouseOverTile.location = mouseOverTile.location;
        } else if (mouseOverTile.tile != previousMouseOverTile.tile && previousMouseOverTile.tile != null) {
            //Debug.Log ("MouseExitFrom " + previousMouseOverTile.tile);
            mouseExitEvent.Invoke (previousMouseOverTile);
            NullPreviousTile ();
            //Debug.Log (previousMouseOverTile.tile);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GridPathfinderEvents : UnityEvent<GridPathfinder, Vector3Int, Vector3Int> { }

[System.Serializable]
public class GridPathfinderMoveStartEvent : GridPathfinderEvents { }

[System.Serializable]
public class GridPathfinderWaypointReachedEvent : GridPathfinderEvents { }

[System.Serializable]
public class GridPathfinderMoveEndEvent : GridPathfinderEvents { }

public class GridPathfinder : MonoBehaviour {
    // Make the originpoint the feet e.g.
    public Transform originPoint;
    // this is what you actually want to move
    public Transform parentTransform;
    public Camera mainCam;
    private bool visualizing = false;
    private bool confirming = false;
    public float moveSpeed = 1f;
    public int maxDistance = 0;
    public bool zeroDistanceAfterMove = true;
    private UnityEngine.Tilemaps.TileBase selectedTile;
    public List<Spot> currentPath = new List<Spot> ();
    public Vector3Int selfLocation;
    private Coroutine mover;
    private Grid maingrid;

    public GridPathfinderMoveStartEvent moveStartedEvent;
    public GridPathfinderMoveEndEvent moveFinishedEvent;
    public GridPathfinderMoveEndEvent waypointReachedEvent;

    // Start is called before the first frame update
    void Start () {
        if (originPoint == null) {
            originPoint = transform;
        }
        if (parentTransform == null) {
            parentTransform = transform;
        }
        if (mainCam == null) {
            mainCam = GridCameraController.instance.mainCam;
        }

        maingrid = GridManager.instance.maingrid;
        selfLocation = maingrid.WorldToCell (originPoint.position);
        parentTransform.position = maingrid.CellToWorld (selfLocation);
    }
    public void ClickedToMove (TileInfo info) {
        if (mover == null) {
            if (PathFindToMouseLocation ()) {
                if (visualizing) {
                    confirming = true;
                    visualizing = false;
                    selectedTile = info.tile;
                } else if (confirming && info.tile == selectedTile) {
                    if (mover == null) {
                        mover = StartCoroutine (Mover ());
                        confirming = false;
                        visualizing = false;
                    };
                } else if (confirming && info.tile != selectedTile) {
                    visualizing = true;
                    confirming = false;
                    selectedTile = info.tile;
                }
            }
        };
    }

    public void StartVisualizing () {
        visualizing = true;
        confirming = false;
    }

    [EasyButtons.Button]
    void SnapToSpotDebug () { // Debug mode!
        Grid grid = GetComponentInParent<Grid> ();
        selfLocation = grid.WorldToCell (originPoint.position);
        parentTransform.position = grid.CellToWorld (selfLocation);
    }

    public bool PathFindToLocation (Vector3 location) {
        List<Spot> path = GridManager.instance.MakePath (originPoint.position, location, maxDistance);

        if (path != null) {
            CreateOwnPath (path);
            return true;
        }
        return false;
    }
    public bool PathFindToMouseLocation () {
        return PathFindToLocation (mainCam.ScreenToWorldPoint (Input.mousePosition));
    }

    public void CreateOwnPath (List<Spot> roadPath) {
        currentPath.Clear ();
        for (int i = roadPath.Count - 1; i > -1; i--) {
            currentPath.Add (roadPath[i]);
            //Debug.Log (i);
        };
    }

    IEnumerator Mover () { // Let's do iiit
        float t = 0f;
        // Gets the current cell and the last cell - for the event
        Vector3Int currentCell = new Vector3Int (currentPath[0].X, currentPath[0].Y, 0);
        Vector3Int nextCell = new Vector3Int (currentPath[currentPath.Count - 1].X, currentPath[currentPath.Count - 1].Y, 0);;
        moveStartedEvent.Invoke (this, currentCell, nextCell);
        // Event done!
        for (int i = 0; i < currentPath.Count; i++) {
            t = 0f;
            //currentPath.SetTile (new Vector3Int (roadPath[i].X, roadPath[i].Y, 0), roadTile);
            currentCell = new Vector3Int (currentPath[i].X, currentPath[i].Y, 0);
            nextCell = currentCell;
            if (i + 1 < currentPath.Count) {
                nextCell = new Vector3Int (currentPath[i + 1].X, currentPath[i + 1].Y, 0);
            } else { // Reached end of waypoints!
                break;
            }
            // Reached waypoint [sort of] - send event!
            waypointReachedEvent.Invoke (this, currentCell, nextCell);

            Vector3 startSpot = maingrid.CellToWorld (currentCell);
            Vector3 endSpot = maingrid.CellToWorld (nextCell);
            while (t < 1f) {
                t += Time.deltaTime * (moveSpeed);
                parentTransform.position = Vector3.Lerp (startSpot, endSpot, t);
                yield return null;
            };
            GridManager.instance.roadMap.SetTile (new Vector3Int (currentCell.x, currentCell.y, 0), null);
        }
        yield return new WaitForSeconds (0.5f);
        // Finished moving, send event!
        moveFinishedEvent.Invoke (this, currentCell, currentCell);
        GridManager.instance.roadMap.SetTile (new Vector3Int (currentPath[currentPath.Count - 1].X, currentPath[currentPath.Count - 1].Y, 0), null);
        mover = null;
        if (zeroDistanceAfterMove) {
            maxDistance = 0;
        }

    }

    // Update is called once per frame
    void Update () {
        selfLocation = maingrid.WorldToCell (originPoint.position);
    }
}
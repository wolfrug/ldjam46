using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// FROM https://bitbucket.org/Sniffle6/tilemaps-with-astar/src/master/
// VIDEO: https://www.youtube.com/watch?time_continue=230&v=HCt_CYOW9jg&feature=emb_logo
public class GridManager : MonoBehaviour {
    public static GridManager instance;
    [SerializeField]
    private Grid maingridObj;
    public Tilemap tilemap;
    public Tilemap roadMap;
    public TileBase roadTile;
    public Vector3Int[, ] spots;
    public Vector2Int start;
    Astar astar;
    public List<Spot> drawnRoadPaths = new List<Spot> ();
    new Camera camera;
    BoundsInt bounds;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (this);
        }
    }
    // Start is called before the first frame update
    void Start () {
        tilemap.CompressBounds ();
        roadMap.CompressBounds ();
        bounds = tilemap.cellBounds;
        camera = Camera.main;

        CreateGrid ();
        astar = new Astar (spots, bounds.size.x, bounds.size.y);
    }

    public Grid maingrid {
        get {
            if (maingridObj == null) {
                maingridObj = GetComponent<Grid> ();
            }
            return maingridObj;
        }
        set {
            maingridObj = value;
        }
    }
    public void CreateGrid () {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++) {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++) {
                if (tilemap.HasTile (new Vector3Int (x, y, 0))) {
                    spots[i, j] = new Vector3Int (x, y, 0);
                } else {
                    spots[i, j] = new Vector3Int (x, y, 1);
                }
            }
        }
    }
    private void DrawRoad (List<Spot> roadPath) {
        for (int i = 0; i < roadPath.Count; i++) {
            roadMap.SetTile (new Vector3Int (roadPath[i].X, roadPath[i].Y, 0), roadTile);
            drawnRoadPaths.Add (roadPath[i]);
        }
    }
    void ClearRoad () {
        for (int i = 0; i < drawnRoadPaths.Count; i++) {
            roadMap.SetTile (new Vector3Int (drawnRoadPaths[i].X, drawnRoadPaths[i].Y, 0), null);
        }
        drawnRoadPaths.Clear ();
    }

    public List<Spot> MakePath (Vector3 startPos, Vector3 endPos, int maxSteps, bool drawRoad = true) {
        // Start pos
        List<Spot> roadPath = new List<Spot> ();
        Vector3Int gridPosStart = tilemap.WorldToCell (startPos);
        start = new Vector2Int (gridPosStart.x, gridPosStart.y);
        // End pos
        CreateGrid ();

        //Vector3 world = camera.ScreenToWorldPoint (Input.mousePosition);
        Vector3Int gridPosEnd = tilemap.WorldToCell (endPos);

        if (drawnRoadPaths != null && drawnRoadPaths.Count > 0) {
            ClearRoad ();
        };

        roadPath = astar.CreatePath (spots, start, new Vector2Int (gridPosEnd.x, gridPosEnd.y), maxSteps);
        if (roadPath == null) {
            //roadPath.Clear ();
            return roadPath;
        }

        if (drawRoad) {
            DrawRoad (roadPath);
        };
        start = new Vector2Int (roadPath[0].X, roadPath[0].Y);
        return roadPath;
    }

    // Update is called once per frame

    void Update () {

    }
}
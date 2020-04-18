using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementGrid : GridController {

    // Add this to the TILEMAP that has your "floor" or other moveable parts.
    public Tilemap movementTilemap;
    [SerializeField]
    private TileInfo highlightedTile;
    public List<TileInfo> highlightedTiles = new List<TileInfo> { };

    void Start () {
        GridCameraController.instance.mouseOverEvent.AddListener (HighLightTile);
        GridCameraController.instance.mouseExitEvent.AddListener (DeHighlightTile);
        GridCameraController.instance.mouseLeftClickEvent.AddListener (ClickTile);
    }

    void NullHighLightInfoTile () {
        highlightedTile.tile = null;
        highlightedTile.tilemap = null;
        highlightedTile.location = Vector3Int.zero;
    }
    void UpdateHighLightTile (TileInfo newinfo) {
        highlightedTile.tile = newinfo.tile;
        highlightedTile.tilemap = newinfo.tilemap;
        highlightedTile.location = newinfo.location;
        highlightedTiles.Add (newinfo);
    }

    void DeSelectAll () {
        foreach (TileInfo info in highlightedTiles) {
            tilemap.SetTileFlags (info.location, TileFlags.None);
            tilemap.SetColor (info.location, Color.white);
        }
        highlightedTiles.Clear ();
    }

    public bool CanMoveToLocation (Vector3 worldSpaceLoc) {
        return HasTile (WorldSpaceToGrid (worldSpaceLoc));
    }

    void HighLightTile (TileInfo info) {
        if (!highlightedTiles.Contains (info)) {
            DeSelectAll ();
            //Debug.Log ("Setting color of" + tilemap.GetTile (info.location));
            tilemap.SetTileFlags (info.location, TileFlags.None);
            tilemap.SetColor (info.location, Color.grey);
            UpdateHighLightTile (info);
        }
    }
    void DeHighlightTile (TileInfo info) {
        if (info.tilemap == highlightedTile.tilemap) {
            DeSelectAll ();
            //Debug.Log ("Resetting color of" + info.tile);
            tilemap.SetTileFlags (info.location, TileFlags.None);
            tilemap.SetColor (info.location, Color.white);
            NullHighLightInfoTile ();
        }
    }

    void ClickTile (TileInfo info) {
        if (info.tilemap == tilemap) {
            //DeSelectAll ();
            //Debug.Log ("Clicked " + info.tile);
            tilemap.SetTileFlags (info.location, TileFlags.None);
            tilemap.SetColor (info.location, Color.green);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
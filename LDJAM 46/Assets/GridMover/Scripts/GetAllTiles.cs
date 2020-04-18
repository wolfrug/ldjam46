﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// FROM https://bitbucket.org/Sniffle6/tilemaps-with-astar/src/master/
// VIDEO: https://www.youtube.com/watch?time_continue=230&v=HCt_CYOW9jg&feature=emb_logo
public class GetAllTiles : MonoBehaviour
{
    public Grid m_Grid;
    private Tilemap map;
    public TileBase tile;
    void Start()
    {
        m_Grid = GetComponent<Grid>();
        map = GameObject.FindObjectOfType<Tilemap>();
    }
    void Update()
    {
        if (m_Grid && Input.GetMouseButton(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = m_Grid.WorldToCell(world);

            map.SetTile(gridPos, tile);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapRenderer : MonoBehaviour, HexTile.IClickCallback
{
    public GameObject TilePrefab;
    public float Gap;
    private float edgeLength;
    private DungeonMapData mapData;

    private GameStatsPersistor persistor;

    private void Awake()
    {
        var bounds = TilePrefab.GetComponent<Renderer>().bounds;
        edgeLength = bounds.size.x / 2;
        persistor = GameObject.FindGameObjectWithTag ("GameStatsPersistor").GetComponent<GameStatsPersistor> ();
        mapData = persistor.DungeonMap;
        mapData.CommitMovement();
    }

    public void RenderMap()
    {
        var map = mapData;
        var center = calcMapCenter(map.Tiles);

        foreach (var tileData in map.Tiles)
        {
            var pos = calcWorldPos(tileData.Pos) - center;
            var tile = Instantiate(TilePrefab, pos, Quaternion.identity, transform);
            var material = tile.GetComponent<Renderer>().material;
              
            if (tileData.Pos.Equals(map.CurrentPosition))
            {
                material.color = Color.cyan;
            }
            else if (map.HasVisited(tileData.Pos))
            {
                material.color = Color.yellow;
            }
            else if (map.NextAvailableSteps().Contains(tileData.Pos))
            {
                material.color = Color.green;
            }

            tile.GetComponent<HexTile>().TileData = tileData;
            tile.GetComponent<HexTile>().Callback = this;
        }
    }

    private Vector2 calcMapCenter(ICollection<TileData> tiles)
    {
        var centerX = (tiles.Max(t => t.Pos.x) - tiles.Min(t => t.Pos.x)) / 2;
        var centerY = (tiles.Max(t => t.Pos.y) - tiles.Min(t => t.Pos.y)) / 2;
        return calcWorldPos(new Vector2Int(centerX, centerY));
    }

    private Vector2 calcWorldPos(Vector2Int pos)
    {
        var x = (pos.x + pos.y) * (1.5 * edgeLength + Gap);
        var y = (pos.y - pos.x) * (Math.Sqrt(3) / 2 * edgeLength + Gap);
        return new Vector2((float) x, (float) y);
    }

    public void OnTileClick(TileData data)
    {
        if (mapData.NextAvailableSteps().Contains(data.Pos))
        {
            mapData.NextPosition = data.Pos;
            SceneManager.LoadScene("fight");
        }
        else
        {
            Debug.Log("You can't go there.");
        }
    }
}

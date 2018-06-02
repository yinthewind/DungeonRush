using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonMapData
{
    public ICollection<TileData> Tiles;
    public Vector2Int CurrentPosition;
    public List<Vector2Int> VisitedPosition;
    public Vector2Int NextPosition;

    public DungeonMapData()
    {
        CurrentPosition = new Vector2Int(0, 0);
        NextPosition = CurrentPosition;
        VisitedPosition = new List<Vector2Int>();
        // Dummy MapGenerator 
        Tiles = new List<TileData>();
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                Tiles.Add(new TileData(i, j));
            }
        }
    }

    public bool HasVisited(Vector2Int pos)
    {
        return VisitedPosition.Contains(pos);
    }

    public List<Vector2Int> NextAvailableSteps()
    {
        var steps = new List<Vector2Int>();
        var nextPos = CurrentPosition + new Vector2Int(1, 0);
        
        if (isPositionInMap(nextPos))
        {
            steps.Add(nextPos);
        }
        nextPos = CurrentPosition + new Vector2Int(0, 1);
        if (isPositionInMap(nextPos))
        {
            steps.Add(nextPos);
        }

        return steps;
    }

    private bool isPositionInMap(Vector2Int pos)
    {
        return Tiles.Any(t => t.Pos.Equals(pos));
    }
    
 
    // Load map from storage deivce.
    public DungeonMapData(string persistMapData)
    {
        // Dummy MapGenerator 
        Tiles = new List<TileData>();
        Tiles.Add(new TileData(0, 0));
        Tiles.Add(new TileData(0, 1));
        Tiles.Add(new TileData(1, 0));
        Tiles.Add(new TileData(1, 1));
    }

    // If we have uncommitted movement (i.e. NextPosition != CurrentPosition) that means we
    // just finished from "fight" scene and won the battle, So we make one step forward here.
    public void CommitMovement()
    {
        if (!NextAvailableSteps().Contains(NextPosition)) return;
        VisitedPosition.Add(CurrentPosition);
        CurrentPosition = NextPosition;
    }
}

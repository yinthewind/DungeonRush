using System;
using UnityEngine;

public class TileData
{
    public readonly Vector2Int Pos;
    // Maybe change to enum;
    public readonly int MonsterType;

    public TileData(int x, int y)
    {
        Pos = new Vector2Int(x, y);
        MonsterType = 0;
    }
}

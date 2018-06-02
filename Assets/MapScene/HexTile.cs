using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class HexTile : MonoBehaviour
{
    public interface IClickCallback
    {
        void OnTileClick(TileData data);
    }
 
    public TileData TileData;
    public IClickCallback Callback;
    
    private void OnMouseUp()
    {
        if (Callback != null)
        {
            Callback.OnTileClick(TileData);
        }
    }
}
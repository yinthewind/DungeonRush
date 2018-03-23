﻿using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
/// <summary>
/// Help to create a GameStatesPersistor
/// </summary>
public static class DebugHelper
{
    public static void initForFightScene()
    {
		GameObject gObject = GameObject.FindGameObjectWithTag("GameStatesPersistor");
		if(gObject == null)
		{
			gObject = new GameObject("GameStatesPersistor");
			gObject.tag = "GameStatesPersistor";
			gObject.AddComponent<GameStatesPersistor>();
		}
    }
}
#endif

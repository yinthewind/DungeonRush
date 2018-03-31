using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Help to create a GameStatsPersistor
/// </summary>
public static class DebugHelper
{
	public static void initForFightScene()
	{
		GameObject gObject = GameObject.FindGameObjectWithTag("GameStatsPersistor");
		if(gObject == null)
		{
			gObject = new GameObject("GameStatsPersistor");
			gObject.tag = "GameStatsPersistor";
			gObject.AddComponent<GameStatsPersistor>();
		}
	}
}
#endif
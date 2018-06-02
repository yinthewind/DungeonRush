using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapScene : MonoBehaviour
{
	public GameObject TilePrefab;
	public MapRenderer MapRenderer;

	private void Start() {
		GameObject.Find ("ItemButton").GetComponent<Button> ().onClick.AddListener (onItemButtonClick);
		renderDungeonMap();
	}

	private void renderDungeonMap()
	{
		MapRenderer = GameObject.Find("MapContainer").GetComponent<MapRenderer>();
		MapRenderer.RenderMap();
	}

	void onItemButtonClick() {
		SceneManager.LoadScene ("Items");
	}
}
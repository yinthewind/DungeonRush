using UnityEngine;
using UnityEngine.UI;

public class TextObject{

	GameObject go;
	public TextObjectRenderer Renderer;

	public TextObject(GameObject parent) {

		this.go = new GameObject ("TextObject");
		this.go.transform.SetParent (parent.transform);
		this.go.transform.position = parent.transform.position;

		this.Renderer = this.go.AddComponent<TextObjectRenderer> ();
	}

	public void Destroy() {
		GameObject.Destroy (this.go);
	}
}

public class TextObjectRenderer : MonoBehaviour
{
	GameObject textObject;
	public Text TextComponent;

	public void Awake() {
		this.gameObject.AddComponent<Canvas> ().sortingOrder = 1;
		this.textObject = new GameObject ("text");
		this.textObject.transform.SetParent (this.gameObject.transform);
		this.textObject.transform.localPosition = new Vector3 (0, 0);

		this.TextComponent = this.textObject.AddComponent<Text>();
		this.TextComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		this.TextComponent.color = Color.blue;
		this.TextComponent.alignment = TextAnchor.MiddleCenter;
		this.TextComponent.fontSize = 20;
	}
}
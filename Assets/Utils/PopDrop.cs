using UnityEngine;
using UnityEngine.UI;

public class PopDropText 
{
	GameObject go;
	Text textComponent;
	PopDropRenderer renderer;

	public PopDropText(GameObject emitter, string text, Color color)
	{
		this.go = new GameObject ("PopDropText");
		go.transform.SetParent (emitter.transform);
		go.transform.position = emitter.transform.position;

		this.textComponent = new TextObject (go).Renderer.TextComponent;
		this.textComponent.text = text;
		this.textComponent.color = color;
		this.textComponent.fontSize = 30;

		this.go.AddComponent<PopDropRenderer> ();
	}
}

public class PopDropRenderer : MonoBehaviour
{
	Rigidbody2D body;
	float duration = 3;

	public void Awake() {
		this.body = this.gameObject.AddComponent<Rigidbody2D> ();

		this.body.velocity = new Vector2 (Random.Range (-150, 150), 400);
		this.body.gravityScale = 125;
	}

	public void Update() {
		if (this.duration < 0) {
			Destroy (this.gameObject);
		}
		this.duration -= Time.deltaTime;


		var newScale = Mathf.Lerp(1.5f, 0.1f, Mathf.Abs(2.7f - duration));
		transform.localScale = new Vector3(newScale, newScale, 1);
	}
}
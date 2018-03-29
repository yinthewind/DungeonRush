using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var canvas = GameObject.FindObjectOfType<Canvas>();
		var exploreButton = canvas.transform.Find("ExploreButton").GetComponent<Button>();
		exploreButton.onClick.AddListener(exploreButtonOnClick);
		var ignoreButton = canvas.transform.Find("IgnoreButton").GetComponent<Button>();
		ignoreButton.onClick.AddListener(ignoreButtonOnClick);
	}

	void exploreButtonOnClick()
	{
		var canvas = GameObject.FindObjectOfType<Canvas>();
		var news = canvas.transform.Find("Text").GetComponent<Text>();
		news.text = "You fall into a pit, and die....";

		canvas.transform.Find("ExploreButton").GetComponent<Button>().enabled = false;
		canvas.transform.Find("IgnoreButton").GetComponent<Button>().enabled = false;

		GameObject nextButton = new GameObject("NextButton");
		nextButton.transform.SetParent(canvas.transform);
		nextButton.transform.position = canvas.transform.position;
		nextButton.AddComponent<Image>().sprite = Resources.Load<Sprite>("Square");
		nextButton.AddComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene("mainMenu"); });

		GameObject text = new GameObject("NextButtonText");
		text.transform.SetParent(nextButton.transform);
		text.transform.position = nextButton.transform.position;
		var textComponent = text.AddComponent<Text>();
		textComponent.text = "Curiosity killed the cat, GG";
		textComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		textComponent.color = Color.black;
		textComponent.fontSize = 15;
	}

	void ignoreButtonOnClick()
	{
		SceneManager.LoadScene("fight");
	}
}

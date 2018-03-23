using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        var buttons = GameObject.FindObjectsOfType<Button>();

        foreach (var button in buttons)
        {
            switch (button.name)
            {
                case "StartButton":
                    button.onClick.AddListener(onStartButtonClick);
                    break;
                case "SettingsButton":
                    button.onClick.AddListener(onSettingsButtonClick);
                    break;
                case "ExitButton":
                    button.onClick.AddListener(onExitButtonClick);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onStartButtonClick()
    {
        initGameStatesPersistor();
        SceneManager.LoadScene("fight");
    }

    void onSettingsButtonClick()
    {
        //SceneManager.LoadScene("");
    }

    void onExitButtonClick()
    {
        Application.Quit();
    }

    void initGameStatesPersistor()
    {
        GameObject gObject = GameObject.FindGameObjectWithTag("GameStatesPersistor");
        if(gObject != null)
        {
            GameObject.Destroy(gObject);
        }
        gObject = new GameObject("GameStatesPersistor");
        gObject.tag = "GameStatesPersistor";
        gObject.AddComponent<GameStatesPersistor>();
    }
}
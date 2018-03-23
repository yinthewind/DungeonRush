using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class FightReport
{
    public GameObject Object;
    FightReportRenderer renderer;

    public FightReport()
    {
        this.Object = new GameObject("FightReport");
        renderer = this.Object.AddComponent<FightReportRenderer>();
    }

    public void DeclareVictory()
    {
        renderer.RenderVictory();

    }
    public void DeclareDefeat()
    {
        renderer.RenderDefeat();
    }
}

public class FightReportRenderer : MonoBehaviour
{
    GameObject Object;
    Text fightReportText;
    Button leaveFightButton;

    private void Start()
    {
        var canvas = GameObject.FindObjectsOfType<Canvas>().Single(c => c.name == "FightScene");
        this.fightReportText = canvas.transform.Find("fightResult").GetComponent<Text>();
        this.fightReportText.enabled = false;

        this.Object = new GameObject("leaveFightButton");
        this.Object.transform.SetParent(canvas.transform);
        this.Object.transform.position = canvas.transform.position;


        this.leaveFightButton = this.Object.AddComponent<Button>();
        this.leaveFightButton.enabled = false;
    }

    public void RenderVictory()
    {
        this.fightReportText.enabled = true;

        this.Object.AddComponent<Image>().sprite = Resources.Load<Sprite>("Square");
        addLeaveFightButtonText();
        this.leaveFightButton.enabled = true;
        this.leaveFightButton.onClick.AddListener(victoryOnClick);
    }

    void victoryOnClick()
    {
        if (Random.value > 0.5)
        {
            SceneManager.LoadScene("fight");
        }
        else
        {
            SceneManager.LoadScene("event");
        }
    }

    public void RenderDefeat()
    {
        this.fightReportText.text = "Defeated!!";
        this.fightReportText.enabled = true;

        this.Object.AddComponent<Image>().sprite = Resources.Load<Sprite>("Square");
        addLeaveFightButtonText();

        this.leaveFightButton.enabled = true;
        this.leaveFightButton.onClick.AddListener(defeatOnClick);
    }

    void defeatOnClick()
    {
        SceneManager.LoadScene("mainMenu");
    }

    void addLeaveFightButtonText()
    {
        var textObject = new GameObject();
        textObject.transform.SetParent(this.Object.transform);
        textObject.transform.position = this.Object.transform.position;

        var textComponent = textObject.AddComponent<Text>();
        textComponent.text = "Leave this fight";
        textComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        textComponent.color = Color.black;
        textComponent.fontSize = 25;
    }
}
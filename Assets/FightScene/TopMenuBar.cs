using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class TopMenuBar
{
    TopMenuBarRenderer renderer;
    
    public TopMenuBar(int level)
    {
        GameObject gameObject = new GameObject("TopMenuBarRenderer");
        this.renderer = gameObject.AddComponent<TopMenuBarRenderer>();
        this.renderer.SetLevelText(level);
    }
}

public class TopMenuBarRenderer : MonoBehaviour
{
    Canvas canvas;
    Text levelText;

    private void Start()
    {
    }

    public void SetLevelText(int level)
    {
        this.canvas = GameObject.FindObjectsOfType<Canvas>().Single(c => c.name == "TopMenuBar");
        this.levelText = this.canvas.transform.Find("Level").GetComponent<Text>();
        this.levelText.text = "Level " + level;
    }
}

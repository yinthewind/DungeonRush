using UnityEngine;
using System.Collections;

public class GameStatesPersistor : MonoBehaviour
{
    public int HitPoint = 250;
    public int Level = 1;
	public Deck Deck = new Deck();

    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

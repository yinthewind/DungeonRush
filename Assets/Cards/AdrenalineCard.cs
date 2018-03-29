using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineCard : Card {

    public AdrenalineCard(){
        this.SpriteName = "AdrenalineCard";
    }

    public override void Play()
    {
        this.FightScene.Player.Energy.Val += 1;
        this.FightScene.Hand.DrawNewCard (2);
        GameObject.Destroy (Object);
    }
}

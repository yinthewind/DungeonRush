using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineCard : Card {

    public AdrenalineCard(){
        this.SpriteName = "AdrenalineCard";
        this.exhausted = true;
    }

    public override void Play()
    {
        this.FightScene.Player.Energy.Val += 1;
        this.FightScene.Hand.DrawNewCard (2);
        base.Play ();
    }
}

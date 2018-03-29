using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackflipCard : Card {

    public BackflipCard(){
        this.SpriteName = "BackflipCard";
    }

    public override void Play(){
        if (this.FightScene.Player.Energy.Val >= 1){

            this.FightScene.Player.Energy.Val -= 1;
            this.FightScene.Player.Shield.Val += 5;

            this.FightScene.Hand.DrawNewCard (2);
            base.Play ();
        }
    }
}

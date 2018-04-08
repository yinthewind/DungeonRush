﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCardsState : State {

	public ExtraCardsState(int duration) : base(StateType.ExtraCards, duration)
	{
	}

	public override void StartTurnEffect(StatesBar states){
		states.ExtraCardsNum += 2;
	}
}
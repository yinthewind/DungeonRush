using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySaveState : State {

	public EnergySaveState(int duration) : base(StateType.EnergySave, duration)
	{
	}

	public override void ApplyEffect(StatesBar states){
		states.EnergyModifier = 0;
	}
}

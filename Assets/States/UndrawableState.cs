using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndrawableState : State {

	public UndrawableState(int duration) : base(StateType.Undrawable, duration)
	{
	}

	public override void ApplyEffect(StatesBar states){
		states.AllowDraw = false;
	}
}

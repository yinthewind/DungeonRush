using System;
using System.Collections.Generic;
using System.Collections;

public enum StateType
{
	Vulnerable,
	Weak,
}

public class StateMeta 
{
	public string Name;
	public string SpriteName;
	public string Comment;
	public int Duration;
}

public static class StateConfigurations
{
	public static Dictionary<StateType, StateMeta> Metas = new Dictionary<StateType, StateMeta> () { {
			StateType.Vulnerable, new StateMeta {
				Name = "Vulnerable",
				SpriteName = "VulnerableState",
				Comment = "Take 50% more damage from attack or spells"
			}
		}, {
			StateType.Weak, new StateMeta {
				Name = "Weak",
				SpriteName = "WeakState",
				Comment = "Dealing 25% less damage"
			}
		}
	};
}
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Fight.Modifier
{
    public class Modifier : ScriptableObject
    {
        // modifier生效时长，单位：回合
        public int Duration;
        // modifier的UI效果名字
        public int EffectName;
        // UI上是否可见（例如：被动卡的modifier不可见）
        public bool IsHidden;

        public bool IsBuff;
        public bool IsDebuff;

        public Event.Event.Trigger Trigger;

        public List<Action.Action> Actions;
    }
}
using UnityEngine;

namespace Framework.Fight.Action
{
    [CreateAssetMenu(fileName = "AddModifier", menuName = "Fight/Action/AddModifier")]
    public class AddModifier : Action
    {
        public Unit Target;
        public Modifier.Modifier Modifier;
    }
}
using UnityEngine;

namespace Framework.Fight.Action
{
    [CreateAssetMenu(fileName = "ProbabilityAction", menuName = "Fight/Action/ProbabilityAction")]
    public class ProbabilityAction : Action
    {
        public float Chance;
        public Fight.Action.Action OnSuccess;
        public Fight.Action.Action OnFailure;
    }
}
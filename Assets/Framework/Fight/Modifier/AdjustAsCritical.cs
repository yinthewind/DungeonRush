using UnityEngine;

namespace Framework.Fight.Action
{
    [CreateAssetMenu(fileName = "AdjustAsCritical", menuName = "Fight/Modifier/AdjustAsCritical")]
    public class AdjustAsCritical : Modifier.Modifier 
    {
        public float CriticalFactor;
    }
}
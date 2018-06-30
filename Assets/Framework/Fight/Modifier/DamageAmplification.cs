using UnityEngine;

namespace Framework.Fight.Modifier
{
    [CreateAssetMenu(fileName = "DamageAmplification", menuName = "Fight/Modifier/DamageAmplification")]
    public class DamageAmplification : Modifier
    {
        public float AmplifyFactor;
    }
}
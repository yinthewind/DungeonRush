using UnityEngine;

namespace Framework.Fight.Event
{
    public class Event : ScriptableObject
    {
        public enum Trigger
        {
            OnCardPlay,
            OnCardExausted,
            OnAttack,
            OnCreated,
            OnHealReceived,
            OnHealthGained,
            OnHeroKilled,
            OnEnergyGained,
            OnOwnerDied,
            OnSpentEnergy,
            OnStateChanged
        }
    }
}
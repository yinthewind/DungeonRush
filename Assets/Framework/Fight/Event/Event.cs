using UnityEngine;

namespace Framework.Fight.Event
{
    public class Event : ScriptableObject
    {
        public enum Trigger
        {
            None,
            OnCardPlay,
            OnCardExausted,
            OnAttack,
            OnPrePhysicalDamage,
            OnCriticalTriggered,
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
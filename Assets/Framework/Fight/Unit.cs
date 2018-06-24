using System.Collections.Generic;
using Framework.Fight.Action;
using Framework.Fight.Event;
using UnityEngine;

namespace Framework.Fight
{
    public class Unit : ScriptableObject
    {
        public int Hp;
        public int MaxHp;
        public int Energy;
        public int Speed;
        public List<Modifier.Modifier> Modifiers;

        public void TakeDamage(Damage damage)
        {
            // EventHandler.OnDamageTaken(damage);
            Hp -= damage.Amount;
            if (Hp <= 0)
            {
                // EventHandler.OnDamageTaken();
            }
        }
    }
}
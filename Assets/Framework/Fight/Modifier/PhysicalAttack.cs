using System.Collections.Generic;
using Framework.Fight.Action;

namespace Framework.Fight.Modifier
{
    public class PhysicalAttack : Modifier
    {
        public void Init()
        {
            Duration = 0;
            IsHidden = true;
            Trigger = Event.Event.Trigger.OnCardPlay;
            Actions = new List<Action.Action>();
            var damage = CreateInstance<Damage>();
            damage.DamageType = Damage.Type.Physical;
            damage.Amount = 10;
            Actions.Add(damage);
        }
    }
}
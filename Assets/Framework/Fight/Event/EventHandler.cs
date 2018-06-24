using System.Collections.Generic;
using System.Linq;
using Framework.Fight.Action;
using Framework.Fight.Card;
using Framework.Fight.Modifier;
using UnityEngine;

namespace Framework.Fight.Event
{
    public class EventHandler : MonoBehaviour
    {
        public void PlayCard(Unit source, Unit target, ActiveCard card)
        {
            card.Modifiers
                .FindAll(m => m.Trigger == Event.Trigger.OnCardPlay)
                .ForEach(m => HandleActions(source, target, m.Actions));
        }

        public void OnDamageTaken(Damage damage)
        {
             damage.Target.Modifiers
                .FindAll(m => m.Trigger == Event.Trigger.OnAttack)
                .ForEach(m => HandleActions(/*damage.Source*/null, damage.Target, m.Actions));
        }

        private void HandleActions(Unit source, Unit target, List<Action.Action> actions)
        {
            HandleAddModifiers(source, target, actions.ConvertAll(m => (AddModifier) m).FindAll(m => m != null));
            HandleDamages(source, target, actions.ConvertAll(m => (Damage) m).FindAll(m => m != null));
        }

        private void HandleAddModifiers(Unit source, Unit target, List<AddModifier> actions)
        {
            foreach (var addModifier in actions)
            {
                addModifier.Target.Modifiers.Add(addModifier.Modifier);
            }
        }

        private void HandleRemoveModifiers(List<RemoveModifier> actions)
        {
            foreach (var removeModifier in actions)
            {
                removeModifier.Target.Modifiers.Remove(removeModifier.Modifier);
            }
        }

        private void HandleDamages(Unit source, Unit target, List<Damage> actions) 
        {
            foreach (var damage in actions)
            {
                CalculateDamage(source, target, damage);
                ApplyDamage(source, target, damage);
            }
        }

        private void CalculateDamage(Unit source, Unit target, Damage damage)
        {

            List<DamageAmplification> amplifications = source.Modifiers
                .ConvertAll(m => (DamageAmplification) m).FindAll(d => d != null);

            List<DamageReduction> reductions = target.Modifiers
                .ConvertAll(m => (DamageReduction) m).FindAll(d => d != null);

            var adjustedFactor = amplifications.ConvertAll(a => a.AmplifyFactor).Aggregate(0f, (a, b) => a + b);
            adjustedFactor -= reductions.ConvertAll(a => a.ReduceFactor).Aggregate(0f, (a, b) => a + b);
            damage.Amount = (int) (damage.Amount * adjustedFactor);
        }

        private void ApplyDamage(Unit source, Unit target, Damage damage)
        {
            OnDamageTaken(damage);
            damage.Target.Hp -= damage.Amount;
            if (damage.Target.Hp <= 0)
            {
               // OnUnitDeath(); 
            }
        }
    }
}
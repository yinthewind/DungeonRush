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
        public Unit Player;
        public Unit Monster;

        public void Trigger(Event.Trigger trigger, List<Action.Action> action,
            List<Modifier.Modifier> modifiers)
        {
            modifiers.FindAll(m => m.Trigger == trigger)
                .ForEach(m => HandleActions(Player, Monster, m.Actions));
        }
        
        public void PlayCard(ActiveCard card)
        {
            Debug.Log("modifier number : " + card.Modifiers.Count);
            if (card.Actions != null)
            {
                HandleActions(Player, Monster, card.Actions);
            }
            if (card.Modifiers != null)
            {
                Trigger(Event.Trigger.OnCardPlay, card.Actions, card.Modifiers);
            }
        }

        public void OnDamageTaken(Damage damage)
        {
             damage.Target.Modifiers
                .FindAll(m => m.Trigger == Event.Trigger.OnAttack)
                .ForEach(m => HandleActions(Player, Monster, m.Actions));
        }

        private void HandleActions(Unit source, Unit target, List<Action.Action> actions)
        {
            HandleAddModifiers(source, target, actions.OfType<AddModifier>().ToList());
            HandleProbabilityEvents(source, target, actions.OfType<ProbabilityAction>().ToList());
            HandleDamages(source, target, actions.OfType<Damage>().ToList());
        }

        private void HandleProbabilityEvents(Unit source, Unit target, List<ProbabilityAction> actions)
        {
            foreach (var probabilityAction in actions)
            {
                float r = Random.Range(0, 1);
                var triggered = r < probabilityAction.Chance;
                var newActions = new List<Action.Action>
                {
                    triggered
                        ? probabilityAction.OnSuccess
                        : probabilityAction.OnFailure
                };
                HandleActions(source, target, newActions);
            }
        }

        private void HandleAddModifiers(Unit source, Unit target, List<AddModifier> actions)
        {
            foreach (var addModifier in actions)
            {
                if (!addModifier.Target.Modifiers.Contains(addModifier.Modifier))
                {
                    addModifier.Target.Modifiers.Add(addModifier.Modifier);
                } 
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
            var amplifications = source.Modifiers.OfType<DamageAmplification>().ToList();
            var reductions = target.Modifiers.OfType<DamageReduction>().ToList();
            var adjustedFactor = amplifications.ConvertAll(a => a.AmplifyFactor).Aggregate(1f, (a, b) => a + b);
            adjustedFactor -= reductions.ConvertAll(a => a.ReduceFactor).Aggregate(0f, (a, b) => a + b);
            damage.Amount = (int) (damage.Amount * adjustedFactor);
        }

        private void ApplyDamage(Unit source, Unit target, Damage damage)
        {
            Debug.Log("Apply damage : " + damage.Amount);
            OnDamageTaken(damage);
            damage.Target.Hp -= damage.Amount;
            if (damage.Target.Hp <= 0)
            {
               // OnUnitDeath(); 
            }
        }
    }
}
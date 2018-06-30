using System.Collections.Generic;
using System.Linq;
using System.Security;
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

        public void Trigger(Event.Trigger trigger, List<Modifier.Modifier> modifiers)
        {
            modifiers.FindAll(m => m.Trigger == trigger)
                .ForEach(m => HandleActions(Player, Monster, m.Actions));
        }
        
        public void PlayCard(ActiveCard card)
        {
            Trigger(Event.Trigger.OnCardPlay, Player.Modifiers);
            HandleActions(Player, Monster, card.Actions);
        }

        public void OnDamageTaken(Unit source, Unit target, Damage damage)
        {
             target.Modifiers
                .FindAll(m => m.Trigger == Event.Trigger.OnAttack)
                .ForEach(m => HandleActions(Player, Monster, m.Actions));
        }

        private void HandleActions(Unit source, Unit target, List<Action.Action> actions)
        {
            HandleAddModifiers(source, target, actions.OfType<AddModifier>().ToList());
            HandleProbabilityActions(source, target, actions.OfType<ProbabilityAction>().ToList());
            HandleDamages(source, target, actions.OfType<Damage>().ToList());
        }

        private void HandleProbabilityActions(Unit source, Unit target, List<ProbabilityAction> actions)
        {
            foreach (var probabilityAction in actions)
            {
                var r = Random.value;
                Debug.Log("p : " + r);
                var triggered = r < probabilityAction.Chance;
                var newActions = new List<Action.Action>
                {
                    triggered
                        ? probabilityAction.OnSuccess
                        : probabilityAction.OnFailure
                };
                if (triggered)
                {
                    Debug.Log("prob triggered");
                }
                HandleActions(source, target, newActions);
            }
        }

        private void HandleAddModifiers(Unit source, Unit target, List<AddModifier> actions)
        {
            foreach (var addModifier in actions)
            {
                Debug.Log("handle add modifier : " + addModifier.Modifier.DebugHint);
                if (!source.Modifiers.Contains(addModifier.Modifier))
                {
                    // Add more modifier merge streategies here : Stack, refresh, ... 
                    Debug.Log("real add modifier : " + addModifier.Modifier.DebugHint);
                    source.Modifiers.Add(addModifier.Modifier);
                } 
            }
        }

        private void HandleRemoveModifiers(Unit source, Unit target, List<RemoveModifier> actions)
        {
            foreach (var removeModifier in actions)
            {
                target.Modifiers.Remove(removeModifier.Modifier);
            }
        }

        private void HandleDamages(Unit source, Unit target, List<Damage> actions) 
        {
            foreach (var damage in actions)
            {
                var damageClone = Instantiate(damage);
                CalculateDamage(source, target, damageClone);
                ApplyDamage(source, target, damageClone);
            }
        }

        private void CalculateDamage(Unit source, Unit target, Damage damage)
        {
            if (damage.DamageType == Damage.Type.Physical)
            {
                Trigger(Event.Trigger.OnPrePhysicalDamage, source.Modifiers);
            }
            var ciriticals = source.Modifiers.OfType<AdjustAsCritical>().ToList();
            var maxCriticalFactor = 1.0f;
            if (ciriticals.Count > 0)
            {
                maxCriticalFactor = ciriticals.ConvertAll(m => m.CriticalFactor).Max();
            }
            
            Debug.Log("ciritical factor : " + maxCriticalFactor);
            if (maxCriticalFactor > 0)
            {
                damage.Amount *= (int) maxCriticalFactor;
            }
            RemoveOutdatedModifiers(source, target);
            if (maxCriticalFactor > 1)
            {
                Trigger(Event.Trigger.OnCriticalTriggered, source.Modifiers);
            }
            
            var amplifications = source.Modifiers.OfType<DamageAmplification>().ToList();
            var reductions = target.Modifiers.OfType<DamageReduction>().ToList();
            var adjustedFactor = amplifications.ConvertAll(a => a.AmplifyFactor).Aggregate(1f, (a, b) => a + b);
            Debug.Log("factor 1 : " + adjustedFactor);
            adjustedFactor -= reductions.ConvertAll(a => a.ReduceFactor).Aggregate(0f, (a, b) => a + b);
            Debug.Log("factor 2 : " + adjustedFactor);
            damage.Amount = (int) (damage.Amount * adjustedFactor);
        }

        private void RemoveOutdatedModifiers(Unit source, Unit target)
        {
            source.Modifiers.RemoveAll(m => m.Duration == 0);
            target.Modifiers.RemoveAll(m => m.Duration == 0);
        }

        private void ApplyDamage(Unit source, Unit target, Damage damage)
        {
            Debug.Log("Apply damage : " + damage.Amount);
            OnDamageTaken(source, target, damage);
            target.Hp -= damage.Amount;
            if (target.Hp <= 0)
            {
               // OnUnitDeath(); 
            }
        }
    }
}
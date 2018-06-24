using System.Collections.Generic;
using Framework.Fight.Modifier;

namespace Framework.Fight.Card
{
    public class SlashCard : ActiveCard
    {
        public void Init()
        {
            Modifiers = new List<Modifier.Modifier>();
            var attack = CreateInstance<PhysicalAttack>();
            attack.Init();
            Modifiers.Add(attack);
        }
    }
}
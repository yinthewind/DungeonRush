using System.Collections.Generic;
using Framework.Fight.Action;

namespace Framework.Fight.Card
{
    public class PassiveCard : Card
    {
        public List<AddModifier> AddModifiers = new List<AddModifier>();
        public override void Play()
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using System.Security.AccessControl;

namespace Framework.Fight.Card
{
    public class ActiveCard : Card
    {
        public List<Modifier.Modifier> Modifiers;
        public List<Action.Action> Actions;
        public override void Play()
        {
            throw new System.NotImplementedException();
        }
    }
}
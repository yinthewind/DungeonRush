using System.Collections.Generic;
using System.Security.AccessControl;

namespace Framework.Fight.Card
{
    public class ActiveCard : Card
    {
        public List<Action.Action> Actions = new List<Action.Action>();
        public override void Play()
        {
            throw new System.NotImplementedException();
        }
    }
}
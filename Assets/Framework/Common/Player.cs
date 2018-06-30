using Framework.Fight;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Common
{
    public class Player : MonoBehaviour
    {
        public Unit Unit;
        void Update()
        {
            transform.Find("Health").GetComponent<Text>().text = Unit.Hp.ToString();

            var modifiersHint = "";
            if (Unit.Modifiers != null)
            {
                foreach (var modifier in Unit.Modifiers)
                {
                    modifiersHint = modifier.DebugHint;
                }
            }

            transform.Find("Modifiers").GetComponent<Text>().text = modifiersHint;
        }
    }
}
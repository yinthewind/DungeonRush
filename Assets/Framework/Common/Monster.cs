using Framework.Fight;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Common
{
    public class Monster : MonoBehaviour
    {
        public Unit Unit;

        void Update()
        {
            transform.Find("Health").GetComponent<Text>().text = Unit.Hp.ToString();
        }
    }
}
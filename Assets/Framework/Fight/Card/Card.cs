using UnityEngine;

namespace Framework.Fight.Card
{
    public abstract class Card : MonoBehaviour
    {
        public string Description;
        public abstract void Play();
    }
}
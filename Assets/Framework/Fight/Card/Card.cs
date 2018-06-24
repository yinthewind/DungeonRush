using UnityEngine;

namespace Framework.Fight.Card
{
    public abstract class Card : ScriptableObject
    {
        public string Description;
        public abstract void Play();
    }
}
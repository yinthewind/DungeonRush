namespace Framework.Fight.Action
{
    public class Damage : Action
    {
        public enum Type
        {
            Physical,
            Magical,
            Pure
        }

        public Type DamageType;
        public int Amount;
        public Unit Target;

        public Damage(Type type, int amount)
        {
            DamageType = type;
            Amount = amount;
        }
    }
}
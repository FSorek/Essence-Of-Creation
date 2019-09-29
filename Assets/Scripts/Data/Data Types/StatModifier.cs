public class StatModifier
{
    public float Value { get; set; }
    public StatModifierType Type { get; private set; }

    public StatModifier(float value, StatModifierType type)
    {
        Value = value;
        Type = type;
    }
}

using Data.Data_Types.Enums;

namespace Data.Data_Types
{
    public class StatModifier
    {
        public StatModifier(float value, StatModifierType type)
        {
            Value = value;
            Type = type;
        }

        public float Value { get; set; }
        public StatModifierType Type { get; }
    }
}
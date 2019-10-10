using System;

public interface ITowerListeningAbility : ITowerAbility
{
    Action<IUnit> ProcAction { get; }
    int MaxProcs { get; }
    Stat Listener { get; }
}

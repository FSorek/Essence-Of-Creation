using System;
using System.Collections.Generic;
using System.Linq;

public class UnitStatusController
{
    private IUnit owner;
    private List<IStatus> currentStatuses;

    public UnitStatusController(IUnit owner)
    {
        this.owner = owner;
        currentStatuses = new List<IStatus>();
    }

    public void Tick()
    {
        for (int i = 0; i < currentStatuses.Count; i++)
        {
            if (currentStatuses[i].Tick(owner))
                currentStatuses.Remove(currentStatuses[i]);
        }
    }

    public void AddStatus(IStatus status)
    {
        if (status.Stacks || !currentStatuses.Contains(status))
            currentStatuses.Add(status);
        else
            currentStatuses.Find(s => s.Equals(status)).Extend();
    }

    public IStatus[] CurrentStatuses => currentStatuses.ToArray();
}

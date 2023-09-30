using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public Team GetTeam()
    {
        return Team.None;
    }

    public Transform GetTransform()
    {
        return null;
    }

    public HealthSystem GetHealthSystem()
    {
        return null;
    }
}

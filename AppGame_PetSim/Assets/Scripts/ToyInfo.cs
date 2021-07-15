using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyInfo : ItemsInfo
{
    public int HappinessValue;
    public override void Use()
    {
        Status.Instance.StatsChange(Status.StatsType.Happiness, HappinessValue);
    }
}

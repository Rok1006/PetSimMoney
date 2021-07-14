using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkInfo : ItemsInfo
{
    public int hydrationValue;
    public override void Use()
    {
        Status.Instance.StatsChange(Status.StatsType.Hydration, hydrationValue);
    }
}
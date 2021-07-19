using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkInfo : ItemsInfo
{
    [SerializeField]
    private int _hydrationValue;
    public int hydrationValue { get { return _hydrationValue; } }
    public override void Use()
    {
        Status.Instance.StatsChange(StatsType.Hydration, _hydrationValue);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInfo : ItemsInfo
{
    [SerializeField]
    private int _hungerValue;
    public int hungerValue { get { return _hungerValue; } }
    public override void Use()
    {
        Status.Instance.StatsChange(StatsType.Hunger, _hungerValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyInfo : ItemsInfo
{
    [SerializeField]
    private int _happinessValue;
    public int happinessValue { get { return _happinessValue; } }
    public override void Use()
    {
        Status.Instance.StatsChange(StatsType.Happiness, _happinessValue);
    }
}

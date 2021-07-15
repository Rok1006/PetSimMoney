using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInfo : ItemsInfo
{
    public int HungerValue;
    public override void Use()
    {
        Status.Instance.StatsChange(Status.StatsType.Hunger, HungerValue);
    }
}

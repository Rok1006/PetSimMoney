using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardData
{
    public List<string> name = new List<string>();
    public List<int> amount = new List<int>();

    public RewardData(List<string> name, List<int> amount)
    {
        this.name = name;
        this.amount = amount;
    }
}

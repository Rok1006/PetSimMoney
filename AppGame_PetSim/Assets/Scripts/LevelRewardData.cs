using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LevelRewardData : RewardData
{
    public int level;

    public LevelRewardData(int level, List<string> name, List<int> amount) : base(name, amount)
    {
        this.level = level;
    }
}

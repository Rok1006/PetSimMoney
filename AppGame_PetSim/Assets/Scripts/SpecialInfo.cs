using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfo : ItemsInfo
{
    [SerializeField]
    private int itemNumber;
    public override void Use()
    {
        switch (itemNumber)
        {
            case 1:
                User.Instance.ExpUP(User.Instance.maxfpValue);
            break;
            case 2:
                for(int i = 0; i < Status.Instance.statsValue.Length; i++)
                {
                    Status.Instance.statsValue[i] = Status.Instance.statsMax[i];
                }
            break;
            case 3:
                GetComponent<TouchObj>().destoryTrash();
            break;
        }
    }
}

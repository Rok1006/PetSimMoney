using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunritureInfo : MonoBehaviour
{
    [SerializeField]
    private FurnitureType _type;
    public FurnitureType type { get { return _type; } }
}

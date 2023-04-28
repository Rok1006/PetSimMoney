using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    [SerializeField]
    private int _ID;
    public int ID { get { return _ID; } }
    [SerializeField]
    private string _title;
    public string title { get { return _title; } }
}

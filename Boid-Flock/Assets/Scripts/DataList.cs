using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataList : ScriptableObject
{
    public string filename;
    [HideInInspector] public string objectname;
    public string heading;
    [HideInInspector] public string objectinteracted;
    [HideInInspector] public float value1;
    [HideInInspector] public float value2;
    public bool hasInit;
    public bool hasValue;
    public bool isJustString;
/*
    public DataList(string filename, string objectname, string heading, float value1, float value2, bool hasInit)
    {
        this.filename = filename;
        this.objectname = objectname;
        this.heading = heading;
        this.value1 = value1;
        this.value2 = value2;
        this.hasInit = hasInit;
    }*/
}

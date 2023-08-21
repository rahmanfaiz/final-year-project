using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Container: MonoBehaviour 
{
    public abstract bool IsWithin(Vector2 point);
    public abstract Vector2 Project(Vector2 vector);
}

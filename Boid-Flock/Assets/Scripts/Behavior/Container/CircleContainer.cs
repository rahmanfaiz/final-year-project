using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleContainer : Container
{
    [SerializeField] Vector2 _center;
    [SerializeField] float _radius;

    public override bool IsWithin(Vector2 point)
    {
        Vector2 distance = point - _center;
        return (distance.magnitude <= _radius);
    }

    public override Vector2 Project(Vector2 vector)
    {
        return _radius * MathHelper.Instance.UnitOf(vector);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(_center, _radius);  
    }
}

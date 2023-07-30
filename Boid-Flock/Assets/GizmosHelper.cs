using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GizmosHelper : MonoBehaviour
{
    [SerializeField] Vector2 _center;
    [SerializeField]  float _radius;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_center, _radius);
    }
}

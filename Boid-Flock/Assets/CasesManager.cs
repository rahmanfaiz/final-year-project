using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasesManager : MonoBehaviour
{

    [SerializeField] BoidManager _boidManager;
    [SerializeField] BoidBehavior[] _behaviors;  
    [SerializeField] Button[] _buttons;   
    [SerializeField] StayInRadiusBehavior _containment;

    public void ApplyCase(int buttonID){
        if(_behaviors != null && _behaviors.Length == _buttons.Length){
            _boidManager.behavior = _behaviors[buttonID];
        }
    }

    public void SetBehavior(BoidBehavior _behavior){
        _boidManager.behavior = _behavior;
    }

    void OnDrawGizmos(){
        Vector2 _center = _containment.center;
        float _radius = _containment.radius;
        Gizmos.DrawWireSphere(_center,_radius);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CasesManager : MonoBehaviour
{

    [SerializeField] BoidManager _boidManager;
    [SerializeField] BoidBehavior[] _behaviors;  
    [SerializeField] Button[] _buttons;
    [SerializeField] TextMeshProUGUI textMesh;

    // [SerializeField] StayInRadiusBehavior _containmentCircle;
    // [SerializeField] StayInBoundary _containmentBox;

    public void ApplyCase(int buttonID){
        if(_behaviors != null && _behaviors.Length == _buttons.Length){
            _boidManager.behavior = _behaviors[buttonID];
        }
    }

    public void SetBehavior(BoidBehavior _behavior){
        _boidManager.behavior = _behavior;
    }

    public void SetTextCase(string caseNumber){
        textMesh.text = caseNumber;
    }

    // void OnDrawGizmos(){
    //     // Vector2 _center = _containment.center;
    //     // float _radius = _containment.radius;
    //     // Gizmos.DrawWireSphere(_center,_radius);
    //     Vector2 _center = (_containmentBox._maxValue - _containmentBox._minValue)*0.5f;
    //     Gizmos.DrawWireCube(_center, _containmentBox._maxValue);
    // }

}

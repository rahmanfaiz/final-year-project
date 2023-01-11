using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Stay in Boundary")]
public class StayInBoundary : BoidBehavior
{
    [SerializeField] public Vector2 _minValue;
    [SerializeField] public Vector2 _maxValue;
    [SerializeField] private float _stayForce = 5f;
    Vector2 move;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        if(agent.transform.position.x < _minValue.x)
        {
            move.x = _stayForce; 
        } 
        else if (agent.transform.position.x > _maxValue.x)
        {
            move.x = -_stayForce;
        }

        if (agent.transform.position.y < _minValue.y)
        {
            move.y = _stayForce;
        }
        else if (agent.transform.position.y > _maxValue.y)
        {
            move.y = -_stayForce;
        }

        return move;
    }

}

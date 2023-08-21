using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Containment")]
public class Containment : BoidBehavior
{
    [SerializeField] float _offset;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        Vector2 containmentMove = Vector2.zero;

        Vector2 future = (Vector2)agent.transform.position + agent.CurrentVelocity;
        if (flock.container.IsWithin(future))
            return Vector2.zero;

        Vector2 boundary = flock.container.Project(agent.CurrentVelocity);

        Vector2 ortho = boundary - future;
        float length = ortho.magnitude + _offset;
        
        ortho = length * ortho;
        Vector2 desiredVector = future + ortho;

        containmentMove = desiredVector - (Vector2)agent.transform.position;
        

        return containmentMove;
    }

}

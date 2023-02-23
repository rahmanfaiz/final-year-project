using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Flee Behavior")]
public class FleeBehavior : BoidBehavior
{
    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        Vector2 velocity = agent.CurrentVelocity;
        Vector2 target = flock.GetMousePosition();

        Vector2 desiredVelocity = (target - (Vector2)agent.transform.position);
        Vector2 steering = desiredVelocity - velocity;

        return -steering;

    }
}

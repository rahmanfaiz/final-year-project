using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Wander")]
public class WanderBehavior : BoidBehavior
{
    public float CircleDistance;
    public float CircleRadius;
    public float AngleChanges;
    private float wanderAngle;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        Vector2 velocity = agent.CurrentVelocity;
        Vector2 circleCenter = velocity;
        circleCenter.Normalize();
        circleCenter *= CircleDistance;

        Vector2 displacement = new Vector2(0, -1);
        displacement *= CircleRadius;

        
        displacement = SetAngle(displacement, wanderAngle);
        
        wanderAngle += (Random.Range(0, 180) * AngleChanges) - (AngleChanges * .5f);

        //Debug.Log("CircleCenter: " + circleCenter);
        //Debug.Log("Displacement: " + displacement);

        Vector2 wanderForce = circleCenter + displacement;

        //Debug.Log("Wander force: " + wanderForce);
        return wanderForce;
    }

    public Vector2 SetAngle(Vector2 vector, float value)
    {
        var len = vector.magnitude;
        vector.x = Mathf.Cos(value) * len;
        vector.y = Mathf.Sin(value) * len;
        return vector;
    }
}

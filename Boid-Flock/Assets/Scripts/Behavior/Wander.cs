using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Wander")]
public class Wander : BoidBehavior
{
    public float CIRCLE_DISTANCE;
    public float CIRCLE_RADIUS;
    public float ANGLE_CHANGE;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        Vector2 velocity = agent.currentVelocity;
        Vector2 circleCenter = velocity;
        circleCenter.Normalize();
        circleCenter *= CIRCLE_DISTANCE;

        Vector2 displacement = new Vector2(0, -1);
        displacement *= CIRCLE_RADIUS;

        float wanderAngle = (Random.Range(0, 180) * ANGLE_CHANGE) - (ANGLE_CHANGE * .5f);

        displacement = SetAngle(displacement, wanderAngle);

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

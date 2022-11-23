using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Avoidance")]
public class AvoidanceBehavior : BoidBehavior
{
    //smoothed steer variable
    Vector2 currentVelocity;
    public float smoothTime = 0.5f;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        //kalau ga ada neighbour, tidak didapatkan vektor yang ngerubah posisinya
        if (context.Count == 0) 
            return Vector2.zero;

        //nambahin semua posisi dari neighbour trus direratain
        Vector2 avoidanceMove = Vector2.zero;
        int _steps = 0;
        foreach (Transform item in context)
        {
            Vector3 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position);
            if (Vector2.SqrMagnitude(closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                _steps++;
                var _distance = Vector2.Distance(agent.transform.position, item.position);
                avoidanceMove += (Vector2) Vector3.Normalize(agent.transform.position - item.position) / Mathf.Pow(_distance, 2);

                //avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
            
        }

        //avoidanceMove = Vector2.SmoothDamp(agent.transform.up, avoidanceMove, ref currentVelocity, smoothTime);

        if (_steps > 0)
        {
            avoidanceMove /= _steps;
        }

        

        return avoidanceMove;

    }
}

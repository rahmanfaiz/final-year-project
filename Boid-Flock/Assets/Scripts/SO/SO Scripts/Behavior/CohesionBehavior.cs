using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Behavior/Cohesion")]
public class CohesionBehavior : BoidBehavior
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
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        //buat offsetnya dari posisi agent
        cohesionMove -= (Vector2)agent.transform.position;

        //biar steer-nya smooth 
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, smoothTime);
        

        return cohesionMove;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidBehavior : ScriptableObject
{
    //agent : boid we're working with //context : list of transform that interact thru collider (like neighbours or obstacles) //flock : the script that handles the flock via boidmanager
    public abstract Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock);
}

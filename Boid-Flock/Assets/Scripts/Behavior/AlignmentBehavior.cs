using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behavior/Alignment")]
public class AlignmentBehavior : BoidBehavior
{
    [SerializeField] private bool _isUsingFactor;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        //kalau ga ada neighbour, tetep di 'alignment' tersebut
        if (context.Count == 0) 
            return agent.transform.up;

        //nambahin semua 'aligment' dari neighbour trus direratain
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count;
        //Debug.Log("Alignment: " + context.Count);
        
        
        return alignmentMove;
        
        
    }
}

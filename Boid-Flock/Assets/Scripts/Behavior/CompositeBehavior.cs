using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Behavior/Composite")]
public class CompositeBehavior : BoidBehavior
{
    public BoidBehavior[] behaviors;

    [Range(0,100)]
    public float[] weights;

    public override Vector2 CalculateMove(BoidAgent agent, List<Transform> context, BoidManager flock)
    {
        //nge-handle jumlah data yang ga sesuai antara behavior dengan weightnya
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //nge-set up move
        Vector2 move = Vector2.zero;


        //iterasi keseluruhan behavior
        for (int i = 0; i < behaviors.Length; i++)
        {
            
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;

            }
        }

        return move;


    }
}

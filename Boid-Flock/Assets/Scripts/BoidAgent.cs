using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BoidAgent : MonoBehaviour
{
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }


    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 positionOffset)
    {
        transform.up = positionOffset;
        transform.position = positionOffset * Time.deltaTime;
    }
}

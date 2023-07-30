using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BoidAgent : MonoBehaviour
{
    public int id;
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    public Vector2 CurrentVelocity;
    [SerializeField] bool canMove = true;
    [SerializeField] float gizmosRadius;
    

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity, Quaternion rotation)
    {
        if (!canMove) return;

        CurrentVelocity = velocity;
        
        transform.up = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
        transform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmosRadius);
    }
}

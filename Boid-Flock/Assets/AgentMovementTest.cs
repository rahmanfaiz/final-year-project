using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgentMovementTest : MonoBehaviour
{

    [SerializeField] BoidAgent agent;
    [SerializeField] float angularSpeed;
    [SerializeField] float radius;
    float angle;
    
    [SerializeField] TextMeshProUGUI UItext;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*agent.transform.Rotate(0,0,0.45f);
        agent.transform.Translate(0,0.01f,0);*/
        //timer += Time.deltaTime;
        
        UItext.text = Time.time.ToString("00");

        angle += angularSpeed * Time.deltaTime;
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
        
        agent.transform.position = Vector3.zero + direction * radius;
        //agent.Move(direction * radius, agent.transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, 1f);
        Gizmos.DrawSphere(this.transform.position, 0.05f);
    }
}

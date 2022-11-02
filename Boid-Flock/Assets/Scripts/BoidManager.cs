using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoidManager : MonoBehaviour
{
    [SerializeField] private bool isPrintingData;
    public BoidAgent boidPrefab;
    List<BoidAgent> boids = new List<BoidAgent>();
    public BoidBehavior behavior;

    const float BOID_DENSITY = 0.1f;
    
    [Range(10, 500)]
    public int boidsQuantity = 250;

    [Range(1f, 100f)]
    public float driveFactor = 10f; //help scale the displacement
    
    [Range(1f, 100f)]
    public float maxSpeed  = 5f;
    public Vector2 minMaxSpeedLimit;

    [Range(1f, 10f)]
    public float perceptionRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    //ease the math load
    float squareMaxSpeed;
    float squarePerceptionRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    // Start is called before the first frame update
    void Start()
    {

        squareMaxSpeed = maxSpeed * maxSpeed;

        squarePerceptionRadius = perceptionRadius * perceptionRadius;
        squareAvoidanceRadius = squarePerceptionRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        

        for (int i = 0; i < boidsQuantity; i++)
        {
            BoidAgent newBoid = Instantiate(boidPrefab,
                Random.insideUnitCircle * boidsQuantity * BOID_DENSITY,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newBoid.name = "Boid" + i;
            boids.Add(newBoid);
        }
        //RandomSpawnInCircle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        foreach (BoidAgent boid in boids)
        {            

            List<Transform> context = GetNearbyObjects(boid);
            Vector2 move = behavior.CalculateMove(boid, context, this);
            move *= driveFactor;

            minMaxSpeedLimit.y = maxSpeed;
            
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * Random.Range(minMaxSpeedLimit.x, minMaxSpeedLimit.y);
            }

            boid.Move(move);

            Vector3 velocityData = move;
            float speedData = move.magnitude;

            if (isPrintingData)
            {
                Debug.Log("speed of " + boid.name + " is " + move);

                string heading = "Boid,Speed";
                CSVManager.Instance.WriteCSV(heading, boid.name, speedData, Time.timeSinceLevelLoad);
            }
            
        }   
    }

    private List<Transform> GetNearbyObjects(BoidAgent boid)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(boid.transform.position, perceptionRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != boid.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }

    private void RandomSpawnInCircle()
    {
        for (int i = 0; i < boidsQuantity; i++)
        {
            BoidAgent newBoid = Instantiate(boidPrefab,
                Random.insideUnitCircle * boidsQuantity * BOID_DENSITY,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newBoid.name = "Boid" + i;
            boids.Add(newBoid);
        }
    }
}

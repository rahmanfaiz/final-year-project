using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public BoidAgent boidPrefab;
    List<BoidAgent> boids = new List<BoidAgent>();
    public BoidBehavior behavior;

    const float BOID_DENSITY = 0.08f;
    
    [Range(10, 500)]
    public int boidsQuantity = 250;

    [Range(1f, 100f)]
    public float driveFactor = 10f; //help scale the displacement
    
    [Range(1f, 100f)]
    public float maxSpeed  = 5f;

    [Range(1f, 10f)]
    public float perceptionRadius = 1.5f;

    [Range(0f, 1f)]
    public float separationRadiusMultiplier = 0.5f;

    //ease the math load
    float squareMaxSpeed;
    float squarePerceptionRadius;
    float squareSeparationRadius;
    public float SquareAvoidanceRadius { get { return squareSeparationRadius; } }


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squarePerceptionRadius = perceptionRadius * perceptionRadius;
        squareSeparationRadius = perceptionRadius * separationRadiusMultiplier * separationRadiusMultiplier;

        RandomSpawnInCircle();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoidManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private bool isPrintingData;
    public DataList speedDataFile;
    public DataList clusterDataFile;

    [Header("Boid References")]
    public BoidAgent boidPrefab;
    List<BoidAgent> boids = new List<BoidAgent>();
    //public BoidBehavior defaultBehavior;
    public BoidBehavior behavior;

    const float BOID_DENSITY = 0.1f;
    
    [Header("Boid Value")]
    [Range(10, 500)]
    public int boidsQuantity = 250;

    [Range(1f, 100f)]
    public float driveFactor = 10f; //help scale the displacement
    
    [Range(1f, 100f)]
    public float maxSpeed  = 5f;

    [Range(1f, 100f)]
    public float minSpeed = 1f;
    //public Vector2 minMaxSpeedLimit;

    [Range(1f, 10f)]
    public float perceptionRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    [Range(0f, 1f)]
    public float velocityVariation = 0.1f;

    //ease the math load
    float squareMaxSpeed;
    float squareMinSpeed;
    float squarePerceptionRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    float noiseOffset;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Screen.width + " " + Screen.height);
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareMinSpeed = minSpeed * minSpeed;

        squarePerceptionRadius = perceptionRadius * perceptionRadius;
        squareAvoidanceRadius = squarePerceptionRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        noiseOffset = Random.Range(-2f, 2f) * 10.0f;

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
            var _currentRotation = boid.transform.rotation;
            var _direction = move;

            

            move *= driveFactor;
            var _noise = Mathf.PerlinNoise(Time.time, noiseOffset);
            //Debug.Log("Noise " + _noise);
            move *= (1f + _noise * velocityVariation);

            if (move.sqrMagnitude < squareMinSpeed)
            {
                move = move.normalized * minSpeed;
            }
/*            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }*/


            var _rotation = Quaternion.FromToRotation(Vector2.up, _direction.normalized);
            if(_rotation != _currentRotation)
            {
                var ip = Mathf.Exp(-4.0f * Time.deltaTime);
                _rotation = Quaternion.Slerp(_rotation, _currentRotation, ip);
            }

            /*            var _noise = Mathf.PerlinNoise(Time.time, noiseOffset);
                        var _noisedVelocity = move * (1f + _noise * velocityVariation);

                        Debug.Log(_noisedVelocity.magnitude);
                        boid.Move(_noisedVelocity, _rotation);*/
            
            Debug.Log(move.magnitude);
            boid.Move(move, _rotation);
            Vector3 velocityData = move;
            float speedData = velocityData.magnitude;

            if (isPrintingData)
            {
                //-----Speed Data------
                //Debug.Log("speed of " + boid.name + " is " + move);
                string objectname = boid.name;
                
                speedDataFile.objectname = objectname;
                speedDataFile.value1 = speedData;
                speedDataFile.value2 = Time.timeSinceLevelLoad;

                CSVManager.Instance.InitAndWriteCSV(speedDataFile);
                //CSVManager.Instance.WriteCSV(heading, boid.name, speedData, Time.timeSinceLevelLoad);

                //-----Cluster Data------

                Debug.Log(Time.timeSinceLevelLoad);
                CSVManager.Instance.InitAndWriteCSV(clusterDataFile);

            }

        }   
    }

    private List<Transform> GetNearbyObjects(BoidAgent boid)
    {


        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(boid.transform.position, perceptionRadius);
        foreach (Collider2D c in contextColliders)
        {
            clusterDataFile.objectname = boid.name;
            if (c != boid.AgentCollider)
            {
                clusterDataFile.objectinteracted = c.name;
                context.Add(c.transform);
            }
            clusterDataFile.value2 = Time.timeSinceLevelLoad;
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

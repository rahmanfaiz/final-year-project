using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoidManager : MonoBehaviour
{
    #region Test 
    /*[Header("Data")]
    [SerializeField] private bool isPrintingData;
    public DataList speedDataFile;
    public DataList clusterDataFile;*/
    #endregion

    [Header("Boid References")]
    public BoidAgent boidPrefab;
    List<BoidAgent> boids = new List<BoidAgent>();
    public BoidBehavior behavior;

    const float BOID_DENSITY = 0.1f;
    
    [Header("Boid Value")]
    [Range(10, 500)]
    public int boidsQuantity = 250;

    [Range(1f, 100f)]
    public float driveFactor = 10f; 
    
    [Range(1f, 100f)]
    public float maxSpeed  = 5f;

    [Range(0.1f, 100f)]
    public float minSpeed = 1f;

    [Range(1f, 10f)]
    public float perceptionRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    [Range(0f, 10f)]
    public float velocityVariation = 0.1f;

    
    /*float squareMaxSpeed;*/
    float squareMinSpeed;
    float squarePerceptionRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    /*float noiseOffset;*/

    Vector2 move;
    [SerializeField] Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        /*squareMaxSpeed = maxSpeed * maxSpeed;*/
        squareMinSpeed = minSpeed * minSpeed;

        squarePerceptionRadius = perceptionRadius * perceptionRadius;
        squareAvoidanceRadius = squarePerceptionRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        /*noiseOffset = Random.Range(-2f, 2f) * 10.0f;*/

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

        foreach (BoidAgent boid in boids)
        {  
            List<Transform> context = GetNearbyObjects(boid);
            move = behavior.CalculateMove(boid, context, this);
            var _currentRotation = boid.transform.rotation;
            var _direction = move;

            move *= driveFactor;
            /*var _noise = Mathf.PerlinNoise(Time.time, noiseOffset);*/
            /*move *= (1f + _noise * velocityVariation);*/

            if (move.sqrMagnitude < squareMinSpeed)
            {
                move = move.normalized * minSpeed;
            }
          

            var _rotation = Quaternion.FromToRotation(Vector2.up, _direction.normalized);
            if(_rotation != _currentRotation)
            {
                var ip = Mathf.Exp(-4.0f * Time.deltaTime);
                _rotation = Quaternion.Slerp(_rotation, _currentRotation, ip);
            }

            //Debug.Log(move.magnitude);
            //Debug.Log(Time.timeSinceLevelLoad);
            
            boid.Move(move, _rotation);

            #region Print Data Test
/*            Vector3 velocityData = move;
            float speedData = velocityData.magnitude;
            if (isPrintingData)
            {
                //-----Speed Data------
                //Debug.Log("speed of " + boid.name + " is " + move);
                string objectname = boid.name;
                
                speedDataFile.objectname = objectname;
                speedDataFile.value1 = speedData.ToString(".000");
                speedDataFile.value2 = Time.timeSinceLevelLoad.ToString(".000");

                CSVManager.Instance.InitAndWriteCSV(speedDataFile);
                //CSVManager.Instance.WriteCSV(heading, boid.name, speedData, Time.timeSinceLevelLoad);

                //-----Cluster Data------

                Debug.Log(Time.timeSinceLevelLoad);
                //CSVManager.Instance.InitAndWriteCSV(clusterDataFile);

            }*/
            #endregion
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

    #region Test 1
    /*   private List<Transform> GetNearbyObjects(BoidAgent boid)
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
               clusterDataFile.value2 = Time.timeSinceLevelLoad.ToString(".000");
           }


           return context;
       }*/
    #endregion

    #region Helper
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

    public Vector2 GetMousePosition()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }

    public void ResetScene(){    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}

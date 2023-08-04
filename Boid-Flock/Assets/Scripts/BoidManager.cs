using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoidManager : MonoBehaviour
{
    
    #region Test 
    //[Header("Data")]
    [SerializeField] private bool isPrintingData;
    //public DataList speedDataFile;
    //public DataList clusterDataFile;
    #endregion

    [Header("Boid References")]
    public Transform StartPos;
    public BoidAgent boidPrefab;
    List<BoidAgent> boids = new List<BoidAgent>();
    public BoidBehavior behavior;

    const float BOID_DENSITY = 0.1f;
    
    [Header("Boid Value")]
    [Range(10, 500)]
    public int boidsQuantity = 250;
    //public int row;
    //public int col;
    //public float spacing;

    [Range(1f, 100f)]
    public float driveFactor = 10f; 
    
    [Range(1f, 100f)]
    public float maxSpeed  = 5f;

/*    [Range(0.1f, 100f)]
    public float minSpeed = 1f;*/

    [Range(1f, 100f)]
    public float perceptionRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    //[Range(0f, 10f)]
    //public float velocityVariation = 0.1f;


    //[SerializeField] List<BoidAgent> _wallContext;
    
    float squareMaxSpeed;
    //float squareMinSpeed;
    float squarePerceptionRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    /*float noiseOffset;*/

    Vector2 move;
    [SerializeField] Camera mainCamera;


    //Pengambilan Data
    public int[,] Mt;
    double gammaM;


    // Start is called before the first frame update
    void Start()
    {
        Mt = new int[boidsQuantity, boidsQuantity];
        gammaM = 0.5 * boidsQuantity * (boidsQuantity - 1);

        squareMaxSpeed = maxSpeed * maxSpeed;
        //squareMinSpeed = minSpeed * minSpeed;

        squarePerceptionRadius = perceptionRadius * perceptionRadius;
        squareAvoidanceRadius = squarePerceptionRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        /*noiseOffset = Random.Range(-2f, 2f) * 10.0f;*/

        /*        for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        //Debug.Log(i);
                        Vector3 startingPos = new Vector3(StartPos.position.x + j * spacing, StartPos.position.y - i * spacing, StartPos.position.z);
                        BoidAgent newBoid = Instantiate(boidPrefab, startingPos, Quaternion.identity, transform);
                        newBoid.name = "Boid" + i;
                        boids.Add(newBoid);

                    }
                }*/


        for (int i = 0; i < boidsQuantity; i++)
        {
            BoidAgent newBoid = Instantiate(boidPrefab,
                Random.insideUnitCircle * boidsQuantity * BOID_DENSITY,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newBoid.name = "Boid" + i;
            newBoid.id = i;
            boids.Add(newBoid);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //reset array
        for (int i = 0; i < boidsQuantity; i++)
        {
            for (int j = 0; j < boidsQuantity; j++)
            {
                Mt[i, j] = 0;
            }
        }

        foreach (BoidAgent boid in boids)
        {  
            List<Transform> context = GetNearbyObjects(boid);
            move = behavior.CalculateMove(boid, context, this);
            var _currentRotation = boid.transform.rotation;
            var _direction = move;

            move *= driveFactor;
            /*var _noise = Mathf.PerlinNoise(Time.time, noiseOffset);*/
            /*move *= (1f + _noise * velocityVariation);*/

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
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
            if (isPrintingData)
            {
                Debug.Log(Time.timeSinceLevelLoad);
                //CSVManager.Instance.InitAndWriteCSV(clusterDataFile);
                foreach(Transform t in context)
                {
                    var adjacentBA = t.GetComponent<BoidAgent>();
                    Mt[boid.id, adjacentBA.id] = 1;
                }

            }

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

        string MtDump = "";
        for (int i = 0; i < boidsQuantity; i++)
        {
            for (int j = 0; j < boidsQuantity; j++)
            {
                MtDump += Mt[i, j].ToString();
            }
        }

        if(isPrintingData)
        {
            var squareOfMt = MathHelper.Instance.MultiplyMatrix(Mt, Mt);
            var gammaT = 0.5 * MathHelper.Instance.FindTrace(squareOfMt, boidsQuantity);
            var alignmentClusteringIndex = gammaT / gammaM;

            string filename = behavior.name + boidsQuantity.ToString();
            CSVManager.Instance.WriteCSV(filename, "alpha,t", alignmentClusteringIndex, Time.timeSinceLevelLoad);
        }


    }

    private List<Transform> GetNearbyObjects(BoidAgent boid)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(boid.transform.position, perceptionRadius);
        
        foreach (Collider2D c in contextColliders)
        {
            //masukin data ke yang lagi dicek
            //clusterDataFile.objectname = boid.name;
            if (c != boid.AgentCollider)
            {
                //masukin data yang lagi berinteraksi
                //clusterDataFile.objectinteracted = c.name;
                context.Add(c.transform);
            }
            //time
            //clusterDataFile.value2 = Time.timeSinceLevelLoad.ToString(".000");
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

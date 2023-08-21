using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordData : MonoBehaviour
{
    [SerializeField] string _schoolName;
    public Vector2 Velocity;

    void OnEnable()
    {
        StartCoroutine(SpeedDataRecorder());
    }

    private IEnumerator SpeedDataRecorder()
    {
        string heading = "xPos" + "," + "yPos" + "xVel" + "," + "yVel" + "," + "timeElapsed";
        YieldInstruction timedWait = new WaitForSeconds(Time.deltaTime);
        Vector3 lastPosition = transform.position;
        float lastTimestamp = Time.time;

        while (enabled)
        {
            yield return timedWait;

            string fileName = this.transform.name + _schoolName;

            Vector2 deltaPosition = (transform.position - lastPosition);
            var deltaTime = Time.time - lastTimestamp;

            // Clean up "near-zero" displacement
            if (Mathf.Approximately(deltaPosition.x, 0f)) 
                deltaPosition.x = 0f;

            if (Mathf.Approximately(deltaPosition.y, 0f)) 
                deltaPosition.y = 0f;

            Velocity = deltaPosition / deltaTime;
            Debug.Log(this.transform.name + " velocity: " + Velocity + ", Time Elapsed: " + Time.time);
            CSVManager.Instance.WritePositionAndVelocityData(fileName, heading, transform.position.x, transform.position.y, Velocity.x, Velocity.y, Time.time);

            lastPosition = transform.position;
            lastTimestamp = Time.time;



        }
    }
}

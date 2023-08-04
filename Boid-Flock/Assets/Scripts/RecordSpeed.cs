using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordSpeed : MonoBehaviour
{
 
void OnEnable() {
    StartCoroutine(SpeedReckoner());
}

public float Speed;
public float UpdateDelay;

private IEnumerator SpeedReckoner() {

    YieldInstruction timedWait = new WaitForSeconds(UpdateDelay);
    Vector3 lastPosition = transform.position;
    float lastTimestamp = Time.time;

    while (enabled) {
        yield return timedWait;

        string fileName = this.transform.name + "_data4111";
        //CSVManager.Instance.WriteCSV(fileName, "speed", this.transform.name, Speed, Time.time );

        Debug.Log( this.transform.name + " Speed: "  + Speed + ", Time Elapsed: " + Time.time );

        var deltaPosition = (transform.position - lastPosition).magnitude;
        var deltaTime = Time.time - lastTimestamp;

        if (Mathf.Approximately(deltaPosition, 0f)) // Clean up "near-zero" displacement
            deltaPosition = 0f;

        Speed = deltaPosition / deltaTime;

        lastPosition = transform.position;
        lastTimestamp = Time.time;

        
        
    }
}
    
}

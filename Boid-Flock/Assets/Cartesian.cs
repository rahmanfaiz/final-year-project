using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cartesian : MonoBehaviour
{
    [SerializeField] GameObject _point;
    [SerializeField] float _distanceBetweenPointMultiplier = 10f;
    [SerializeField] float _howManyPointUsed = 20;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _howManyPointUsed/2; i++)
        {
            var targetPosition = new Vector3(i * _distanceBetweenPointMultiplier, 0, 0);
            Instantiate(_point, targetPosition, Quaternion.identity);
            Instantiate(_point, -targetPosition, Quaternion.identity);
        }

        for (int i = 0; i < _howManyPointUsed / 2; i++)
        {
            var targetPosition = new Vector3(0, i * _distanceBetweenPointMultiplier, 0);
            Instantiate(_point, targetPosition, Quaternion.identity);
            Instantiate(_point, -targetPosition, Quaternion.identity);
        }
    }

}

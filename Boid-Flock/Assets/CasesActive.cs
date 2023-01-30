using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasesActive : MonoBehaviour
{

    [SerializeField] CasesManager _casesManager;
    [SerializeField] bool isActive = true;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) & isActive){
            _casesManager.gameObject.SetActive(false);
            isActive = false;
        } 
        
        else if(Input.GetKeyDown(KeyCode.Space) & !isActive){
            _casesManager.gameObject.SetActive(true);
            isActive = true;
        }
    }
}

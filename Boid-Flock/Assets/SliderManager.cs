using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private GameObject _sliderTemplate;
    public GameObject[] sliders;
    private Slider _slider;
    public CompositeBehavior composite;

    // Start is called before the first frame update
    void Start()
    {
        sliders = new GameObject[composite.behaviors.Length];
        //Debug.Log(composite.behaviors[1].ToString());
        for (int i = 0; i < composite.behaviors.Length; i++)
        {
            //GameObject slider = Instantiate(_sliderTemplate, transform);
            sliders[i] = Instantiate(_sliderTemplate, transform) as GameObject;
            Text sliderText = sliders[i].GetComponentInChildren<Text>();
            sliderText.text = composite.behaviors[i].ToString();
            _slider = sliders[i].GetComponentInChildren<Slider>();
            _slider.maxValue = 10;
            _slider.value = composite.weights[i];
            sliders[i].name = composite.behaviors[i].ToString();
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

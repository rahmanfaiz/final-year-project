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
    private TextMeshProUGUI _slideValueText;
    private SliderValue _sliderValue;
    public CompositeBehavior composite;

    // Start is called before the first frame update
    void Start()
    {
        InitSliders();        
    }

    public void InitSliders()
    {
        sliders = new GameObject[composite.behaviors.Length];
        //Debug.Log(composite.behaviors[1].ToString());
        for (int i = 0; i < composite.behaviors.Length; i++)
        {

            sliders[i] = Instantiate(_sliderTemplate, transform) as GameObject;
            sliders[i].name = composite.behaviors[i].ToString();

            
            TextMeshProUGUI sliderTextName = sliders[i].transform.Find("Component").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI sliderTextValue = sliders[i].transform.Find("Value").GetComponent<TextMeshProUGUI>();
            //Debug.Log(sliders[i].transform.name);
            sliderTextName.text = composite.behaviors[i].name;
            sliderTextValue.text = composite.weights[i].ToString(".00");

            _slider = sliders[i].GetComponentInChildren<Slider>();
            _sliderValue = _slider.GetComponent<SliderValue>();
            _sliderValue.index = i;

            _slider.maxValue = 10;
            _slider.value = composite.weights[i];
            

        }
    }

}

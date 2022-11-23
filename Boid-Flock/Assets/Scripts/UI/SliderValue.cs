using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public int index;
    [SerializeField] private Slider mySlider;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private CompositeBehavior behavior;

    public float snapInterval = 0.1f;

    private void Start()
    {
        mySlider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0.00");
        });
/*        mySlider.onValueChanged.AddListener(delegate { SliderSnap(); });
        SliderSnap();*/
    }

    public void SliderSnap()
    {
        float value = mySlider.value;
        value = Mathf.Round(value / snapInterval) * snapInterval;
        mySlider.value = value;
    }

    public void onValueCheck()
    {
        behavior.weights[index] = mySlider.value;
    }
}

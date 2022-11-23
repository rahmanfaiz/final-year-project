using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseImage;
    [SerializeField] private Toggle pauseToggle;

    private void Update()
    {
        pauseToggle.onValueChanged.AddListener(delegate { OnValueChanged(); });
        OnValueChanged();
    }

    private  void OnValueChanged()
    {
        if (pauseToggle.isOn)
        {
            PauseGame();
        } else if (!pauseToggle.isOn)
        {
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        pauseImage.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void ContinueGame()
    {
        pauseImage.SetActive(true);
        Time.timeScale = 1;
    }
    
}

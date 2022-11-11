using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject objectToOnOff;

    public void GameObjectActiveToggle()
    {
        objectToOnOff.SetActive(toggle.isOn);
    }

}

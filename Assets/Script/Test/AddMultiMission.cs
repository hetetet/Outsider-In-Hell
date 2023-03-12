using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Localization.Settings;

public class AddMultiMission : MonoBehaviour
{
    [SerializeField] Mission m1;

    [SerializeField] Mission m2;
    void Start()
    {
        MissionAlarm.Instance.show_mission(m1);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            MissionAlarm.Instance.show_mission(m2);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Icon_Mission.Instance.deleteMission(m2);
            Debug.Log("clear "+m2.name);
        }
    }
}

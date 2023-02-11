using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TodoElementBehavior : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI missionName;
    void Start()
    {
        //missionName=GetComponentInChildren<TextMeshProUGUI>();
    }

    public void setMissionName(string name)
    {
        Debug.Log("setmissionname: "+name);
        this.name=name;
        missionName.text = name;
    }
}

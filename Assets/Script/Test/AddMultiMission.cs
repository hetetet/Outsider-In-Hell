using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMultiMission : MonoBehaviour
{
    [SerializeField] Mission m1;

    [SerializeField] Mission m2;
    void Start()
    {
        Icon_Mission.Instance.addMission(m1);
        Icon_Mission.Instance.addMission(m2);
        Icon_Mission.Instance.addMission(m2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

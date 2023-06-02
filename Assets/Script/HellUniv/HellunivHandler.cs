using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellunivHandler : MonoBehaviour
{
    [SerializeField] private Transform FromMap01toHere;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.Find("Soyeon");
    }
    void Start()
    {
        //이전 씬에 따른 현재 씬 위치 조정
        if (PortalBehavior.prevScenename == "Main_map01")
            Player.transform.position = FromMap01toHere.position;

        PortalBehavior.prevScenename = "HellUniv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

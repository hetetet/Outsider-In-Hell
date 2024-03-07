using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HellunivHandler : MonoBehaviour
{
    [SerializeField] private Transform HelluivExit;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.Find("Soyeon");
    }
    void Start()
    {
        Debug.Log("prevscenename: "+PortalBehavior.prevScenename);
        //이전 씬에 따른 현재 씬에서의 플레이어 위치 조정
        if (PortalBehavior.prevScenename == "Main_map01")
            Player.transform.position = HelluivExit.position;
        else if(PortalBehavior.prevScenename == "Starter")
            Player.transform.position = HelluivExit.position;
        else
            Player.transform.position = new Vector3(65f, -8.6f, 0.06f); 

        PortalBehavior.prevScenename = "HellUniv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

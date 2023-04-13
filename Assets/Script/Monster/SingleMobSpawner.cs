using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMobSpawner : MonoBehaviour
{
    //SingleMobSpawner는 하나의 몬스터만 관리. 몬스터가 죽으면 5초 딜레이 후 스포너 위치에 다시 부활시킴
    /*나중에 MultiMobSpawner 만들면 얘는 지가 만든 몬스터가 뒤지든 말든 일정 시간 간격으로 계속 만들까?
    라기에는 시간 지나면 몬스터가 너무 많아저서 렉 뒤지게 걸릴수도 있으니 지가 만든애중 몇명이 살아있는지는 좀 점검을...
    그리고 static 변수를 두어 씬에 총 몇마리의 몬스터가 있는지도 총체적으로 볼수있게 하는 편이 좋을듯*/
    [SerializeField] Enemy emeny;
    private bool IsMobDead = false;
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
        //몬스터가 죽었다는 정보를 받으면 5초 기다리고 몬스터를 부활시킨다.
    }
    public IEnumerator revive()
    {
        yield return new WaitForSeconds(5);
        spawn();
    }

    public void spawn()
    {
        Instantiate(emeny, gameObject.transform);
        IsMobDead = false;
    }
}

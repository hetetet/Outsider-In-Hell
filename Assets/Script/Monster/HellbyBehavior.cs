using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellbyBehavior : Enemy
{

    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        AttackPower = 100;
        Maxhp = 15;
        Hp = Maxhp;
        
        Invoke("think", 3);
    }
    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
 
    //        new WaitForSeconds(1);
    //        Debug.Log("Hellby가 플레이어와 닿았습니다");
    //    }
    //}
}

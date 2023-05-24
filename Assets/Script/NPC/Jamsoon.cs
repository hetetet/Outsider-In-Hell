using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Jamsoon : NpcBehavior
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    [YarnCommand("set_state")]
    public void set_state(int state)
    {
        anim.SetInteger("state", state);  
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using DG.Tweening;

public class Jamsoon : NpcBehavior
{
    
    [SerializeField] Item monmask;
    [SerializeField] ParticleSystem ps;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        ps.Stop();
    }
    [YarnCommand("set_state")]
    public void set_state(int state)
    {
        Anim.SetInteger("state", state);  
    }
    [YarnCommand("give_mask")]
    public void give_mask()
    {
        BackpackManager.add(monmask);
        Dr.onDialogueComplete.AddListener(() =>
        {
            Debug.Log("나 간다~ 뺑이ㅊ.. 아니 잘있어!");
            ps.Stop();
            Sr.DOFade(0, 2f).OnComplete(()=> {
                
                Destroy(gameObject);
            }); 
        });
    }
    [YarnCommand("show_particle")]
    public void show_particle()
    {
        ps.Play();
    }

}

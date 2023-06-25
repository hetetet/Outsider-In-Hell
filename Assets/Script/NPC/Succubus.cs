using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Succubus : NpcBehavior
{
    [SerializeField] Item pic;
    [SerializeField] Mission findman;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
    }

    [YarnCommand("give_pic")]
    public void give_pic()
    {
        BackpackManager.add(pic);
        Dr.onDialogueComplete.AddListener(() =>
        {
            Debug.Log("succubus ������ �� ���� ���ϳ� ��������������");
            MissionAlarm.Instance.show_mission(findman);
        });
    }
}

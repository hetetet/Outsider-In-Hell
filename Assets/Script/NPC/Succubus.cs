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
            Debug.Log("succubus 새끼야 왜 반응 안하냐 ㅅㅄㅄㅄㅄㅄㅄ");
            MissionAlarm.Instance.show_mission(findman);
        });
    }
}

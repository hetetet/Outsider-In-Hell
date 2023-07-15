using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Succubus : NpcBehavior
{
    [SerializeField] Item pic;
    [SerializeField] Mission findman;
    [SerializeField] InMemoryVariableStorage variableStorage;
    // Start is called before the first frame update
    int meet;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        meet = PlayerPrefs.GetInt("meetnpc_succubus", 0);
        variableStorage.SetValue("$meetnpc_succubus", meet);
    }

    [YarnCommand("give_pic")]
    public void give_pic()
    {
        BackpackManager.add(pic);
        Dr.onDialogueComplete.AddListener(() =>
        {
            MissionAlarm.Instance.show_mission(findman);
            if (meet == 0)
            {
                PlayerPrefs.SetInt("meetnpc_succubus", 1);
                variableStorage.SetValue("$meetnpc_succubus", 1);
            }
            else
            {
                Debug.Log("meet is not 0");
            }
        });
    }
}

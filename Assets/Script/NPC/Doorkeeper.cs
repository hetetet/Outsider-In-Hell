using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Doorkeeper : NpcBehavior
{
    GameObject Player;
    Rigidbody2D rigid;
    [SerializeField] Item mask;
    [SerializeField] InMemoryVariableStorage variableStorage;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dr.onDialogueComplete.RemoveAllListeners();
        Dr.onDialogueComplete.AddListener(() => PlayerBehavior.Instance.InitState());
        Debug.Log("start Dialog :: meetplayer: " + meetPlayer.ToString() + ", dr.IsDialogueRunning: " + Dr.IsDialogueRunning);
        if(Icon_Backpack.Mask != null)Debug.Log(Icon_Backpack.Mask.name);
        variableStorage.SetValue("$wearmonmask", ((Icon_Backpack.Mask != null)&&(Icon_Backpack.Mask.name== "Monmask")));
        Dr.StartDialogue("Doorkeeper");
        PlayerBehavior.Instance.DisableMove();
    }

    [YarnCommand("deprive_mask")]
    public void deprive_mask()
    {
        BackpackManager.Remove(mask); //������ ��Ͽ��� ����ũ ����
        Icon_Backpack.Mask=null; //���� �������� ����ũ ����
        Destroy(PlayerBehavior.Instance.MaskArea.GetChild(0).gameObject); //������ ����ũ ����
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

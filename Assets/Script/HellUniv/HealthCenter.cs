using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HealthCenter : MonoBehaviour
{
    bool meetplayer=false;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] InMemoryVariableStorage variableStorage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(meetplayer && (Input.GetKeyUp(KeyCode.LeftShift) || (Input.GetKeyUp(KeyCode.RightShift))))
        {
            if(Icon_Backpack.Instance.Mask != null)
                Debug.Log("Icon_Backpack.Instance.Mask.name: " + Icon_Backpack.Instance.Mask.name);
            variableStorage.SetValue("$isfullhealth", PlayerBehavior.currentHP == PlayerBehavior.maxHP);
            variableStorage.SetValue("$hasmonmask", (Icon_Backpack.Instance.Mask != null && Icon_Backpack.Instance.Mask.name == "Monmask"));
            dialogueRunner.onDialogueComplete.RemoveAllListeners();
            dialogueRunner.StartDialogue("HealthCenter");
            
        }
    }

    [YarnCommand("fill_health")]
    public static void fill_health()
    {
        PlayerBehavior.currentHP = PlayerBehavior.maxHP;
        HPmanager.Instance.showHpBar(PlayerBehavior.currentHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        meetplayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        meetplayer=false;
    }
}

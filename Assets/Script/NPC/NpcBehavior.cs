using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NpcBehavior : MonoBehaviour
{
    [SerializeField] DialogueRunner dr;
    Animator anim;
    SpriteRenderer sr;
    public DialogueRunner Dr { get { return dr; } protected set { dr = value; } }
    public Animator Anim { get { return anim; } protected set { anim = value; } }
    public SpriteRenderer Sr { get { return sr; } protected set { sr = value; } }
    [SerializeField] string currentDialogName;
    private bool meetPlayer;
    
    // Update is called once per frame
    // 대화의 진행 정도, 사라졌는지
   protected virtual void Update()
    {
        
        if (meetPlayer && Input.GetKeyDown(KeyCode.Q) && !dr.IsDialogueRunning)
        {
            Debug.Log("start Dialog :: meetplayer: " + meetPlayer.ToString() + ", dr.IsDialogueRunning: " + dr.IsDialogueRunning);
            dr.StartDialogue(currentDialogName);
            PlayerBehavior.canmove = false;
            dr.onDialogueComplete.RemoveAllListeners();
            dr.onDialogueComplete.AddListener(() =>PlayerBehavior.canmove=true);
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("start Dialog :: meetplayer: " + meetPlayer.ToString() + ", dr.IsDialogueRunning: " + dr.IsDialogueRunning);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            meetPlayer = true;
            Debug.Log("meetplayer is true");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            meetPlayer = false;
            Debug.Log("meetplayer is false");
        }
    }
}

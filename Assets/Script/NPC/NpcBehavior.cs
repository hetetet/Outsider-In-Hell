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
   protected virtual void Update()
    {
        if (meetPlayer && Input.GetKeyDown(KeyCode.Q) && !dr.IsDialogueRunning)
        {
            Debug.Log("start Dialog");
            dr.StartDialogue(currentDialogName);
            dr.onDialogueComplete.RemoveAllListeners();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            meetPlayer = true;
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            meetPlayer = false;
    }
}

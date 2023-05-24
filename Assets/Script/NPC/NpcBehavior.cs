using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NpcBehavior : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] string currentDialogName;
    private bool meetPlayer;
    


    // Update is called once per frame
    void Update()
    {
        if (meetPlayer && Input.GetKeyDown(KeyCode.Q) && !dialogueRunner.IsDialogueRunning)
        {
            Debug.Log("start Dialog");
            dialogueRunner.StartDialogue(currentDialogName);
            dialogueRunner.onDialogueComplete.RemoveAllListeners();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            meetPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            meetPlayer = false;
    }
}

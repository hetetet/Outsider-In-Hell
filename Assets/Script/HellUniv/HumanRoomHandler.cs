using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HumanRoomHandler : MonoBehaviour
{
    [SerializeField] Transform SoyeonPos;
    [SerializeField] DialogueRunner dialogueRunner;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.Find("Soyeon");
    }
    void Start()
    {
        Player.transform.position = SoyeonPos.position;
        dialogueRunner.StartDialogue("HumanRoom_FirstEnter");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

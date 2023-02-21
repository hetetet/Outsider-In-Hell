using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Map01Handler : MonoBehaviour
{
    private GameObject Player;
    private Transform FromStarterToHere;
    [SerializeField] DialogueRunner dialogueRunner;
    private void Awake()
    {
        Player = GameObject.Find("Soyeon");
        FromStarterToHere = GameObject.Find("FromStartToHere").transform;
    }
    void Start()
    {
        Debug.Log("entered map01");
        PlayerBehavior.Instance.DisableMove();
        dialogueRunner.StartDialogue("MainMap01Enter");
        dialogueRunner.onDialogueComplete.AddListener(PlayerBehavior.Instance.EnableMove);

        Player.transform.position = FromStarterToHere.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

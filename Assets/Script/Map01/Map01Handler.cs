using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Map01Handler : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private Transform FromStarterToHere;
    [SerializeField] private Transform FromHellunivToHere;
    [SerializeField] Mission DoZeroTwo;
    [SerializeField] DialogueRunner dialogueRunner;
    private void Awake()
    {
        Player = GameObject.Find("Soyeon");

    }
    void Start()
    {
        int dialog_start = PlayerPrefs.GetInt("dialog_map01", 0);
        Debug.Log("entered map01");
        if (dialog_start == 0)
        {
            PlayerBehavior.Instance.DisableMove();
            dialogueRunner.StartDialogue("MainMap01Enter");
            dialogueRunner.onDialogueComplete.AddListener(() =>
            {
                PlayerBehavior.Instance.EnableMove();
                MissionAlarm.Instance.show_mission(DoZeroTwo);
            });
            PlayerPrefs.SetInt("dialog_map01", 1);
        }
        
        //���� ���� ���� ���� �� ��ġ ����
        if (PortalBehavior.prevScenename == "HellUniv")
            Player.transform.position = FromHellunivToHere.position;
        else
            Player.transform.position= FromStarterToHere.position;
        PortalBehavior.prevScenename = "Main_map01";
    }

    // Update is called once per frame
    void Update()
    {

    }
}

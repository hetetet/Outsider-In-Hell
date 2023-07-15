using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleHandler : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button DeleteButton;
    [SerializeField] private Button SettingButton;

    void Awake()
    {

    }

    private void Start()
    {
        startButton.onClick.AddListener(startGame);
        DeleteButton.onClick.AddListener(DeleteGame);
        SettingButton.onClick.AddListener(GameManager.Instance.ShowSettings);

        GameManager.isSettingDisabled = false;
        Icon_Mission.Missions.Clear();
        BackpackManager.Items.Clear();

        GameManager.setCursorToArrow();
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 1);
        UIEffect.Instance.Fade(0, 1);
    }

    public void startGame() {
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 0);
        
        //가져와야 할 정보 목록
        //플레이중이었던 씬
        string CurrentScene=PlayerPrefs.GetString("CurrentScene", "none");
        //hp
        int Hp=PlayerPrefs.GetInt("Hp", 100);
        //maxhp
        int MaxHp=PlayerPrefs.GetInt("MaxHp", 100);
        //미션 목록
        string misarr= PlayerPrefs.GetString("Missions", "none");
        //아이템 목록
        string itemarr=PlayerPrefs.GetString("Items", "none");
        //얘네둘은 타이틀 화면에서 못 불러오는데 어떡하지...? uieffect의 aftereffect?
        //npc별 대화 여부는 대화 후 따로 저장

        if (CurrentScene=="none") 
            UIEffect.Instance.Fade(1, 2, "Prologue");
        else
        {
            UIEffect.Instance.Fade(1, 2, CurrentScene);
            UIEffect.afterEffect = delegate
            {
                //add missions and items
                string[] items = itemarr.Split("#");
            };
        }
    }

    public void DeleteGame()
    {
        Debug.Log("Delete gay");
        GameManager.Instance.DeleteData();
    }
}

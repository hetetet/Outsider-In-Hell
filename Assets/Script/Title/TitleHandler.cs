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
        
        //�����;� �� ���� ���
        //�÷������̾��� ��
        string CurrentScene=PlayerPrefs.GetString("CurrentScene", "none");
        //hp
        int Hp=PlayerPrefs.GetInt("Hp", 100);
        //maxhp
        int MaxHp=PlayerPrefs.GetInt("MaxHp", 100);
        //�̼� ���
        string misarr= PlayerPrefs.GetString("Missions", "none");
        //������ ���
        string itemarr=PlayerPrefs.GetString("Items", "none");
        //��׵��� Ÿ��Ʋ ȭ�鿡�� �� �ҷ����µ� �����...? uieffect�� aftereffect?
        //npc�� ��ȭ ���δ� ��ȭ �� ���� ����

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

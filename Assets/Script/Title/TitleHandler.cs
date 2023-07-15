using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
public class TitleHandler : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button SettingButton;
    //delete data
    public Button deleteDataBtn;
    public Canvas deleteData;
    public TextMeshProUGUI deleteTitle;
    public Button deleteOk;
    public Button deleteCancel;
    public Animator Anim;

    void Awake()
    {

    }

    private void Start()
    {
        deleteDataBtn.onClick.AddListener(OpenDeleteDataPopup);
        deleteData.gameObject.SetActive(false);
        startButton.onClick.AddListener(startGame);
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
                foreach(string item in items)
                {
                    string[] iteminfo = item.Split('_');
                    //BackpackManager.Items.Add();
                }
            };
        }
    }

    public void DeleteGame()
    {
        Debug.Log("Delete gay");
        DeleteData();
    }
    public void DeleteData()
    {
        deleteOk.gameObject.SetActive(false);
        deleteCancel.gameObject.SetActive(false);
        Anim.SetTrigger("eat");
        Debug.Log("Delete game data");
        PlayerPrefs.DeleteAll();
        StartCoroutine("CoDeleteData");
    }

    IEnumerator CoDeleteData()
    {
        yield return new WaitForSeconds(2f); //������ �Դ� �ִϸ��̼� �ð�����
        deleteOk.gameObject.SetActive(true);
        deleteOk.onClick.RemoveAllListeners();
        deleteOk.onClick.AddListener(CloseDeleteDataPopup);
        deleteTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "datadeleted");
    }
    public void OpenDeleteDataPopup()
    {
        Anim.SetTrigger("init");

        deleteData.gameObject.SetActive(true);
        deleteOk.gameObject.SetActive(true);
        deleteCancel.gameObject.SetActive(true);

        deleteTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "askdeletedata");
       
        deleteOk.onClick.RemoveAllListeners();
        deleteCancel.onClick.RemoveAllListeners();
        deleteCancel.onClick.AddListener(CloseDeleteDataPopup);
        deleteOk.onClick.AddListener(DeleteData);
    }
    public void CloseDeleteDataPopup()
    {
        deleteData.gameObject.SetActive(false);
        deleteOk.onClick.RemoveAllListeners();
        deleteCancel.onClick.RemoveAllListeners();
    }
}
